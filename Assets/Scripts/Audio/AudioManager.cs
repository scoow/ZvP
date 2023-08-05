using UnityEngine;
using UnityEngine.Audio;

// This class differs from the original by using alternatively implemented pool
// Original: https://github.com/UnityTechnologies/open-project-1/blob/main/UOP1_Project/Assets/Scripts/Audio/AudioManager.cs

public class AudioManager : MonoBehaviour
{
	[Header("SoundEmitters pool")]
	private ObjPool<PoolableSoundEmitter> _pool = default;
	[SerializeField] private PoolableSoundEmitter _soundEmitterPrefab;
    [SerializeField] private int _initialSize = 10;
    [SerializeField] private int _maxPoolSize = 50;

    [Header("Listening on channels")]
	[Tooltip("The SoundManager listens to this event, fired by objects in any scene, to play SFXs")]
	[SerializeField] private AudioCueEventChannelSO _SFXEventChannel = default;
	[Tooltip("The SoundManager listens to this event, fired by objects in any scene, to play Music")]
	[SerializeField] private AudioCueEventChannelSO _musicEventChannel = default;
	[Tooltip("The SoundManager listens to this event, fired by objects in any scene, to change SFXs volume")]
	[SerializeField] private FloatEventChannelSO _SFXVolumeEventChannel = default;
	[Tooltip("The SoundManager listens to this event, fired by objects in any scene, to change Music volume")]
	[SerializeField] private FloatEventChannelSO _musicVolumeEventChannel = default;
	[Tooltip("The SoundManager listens to this event, fired by objects in any scene, to change Master volume")]
	[SerializeField] private FloatEventChannelSO _masterVolumeEventChannel = default;


	[Header("Audio control")]
	[SerializeField] private AudioMixer audioMixer = default;
	[Range(0f, 1f)]
	[SerializeField] private float _masterVolume = 1f;
	[Range(0f, 1f)]
	[SerializeField] private float _musicVolume = 1f;
	[Range(0f, 1f)]
	[SerializeField] private float _sfxVolume = 1f;

	private SoundEmitterVault _soundEmitterVault;
	private AudioCueKey _musicAudioKey = AudioCueKey.Invalid;

	private void Awake()
	{
		//TODO: Get the initial volume levels from the settings
		_soundEmitterVault = new SoundEmitterVault();

        _pool = new ObjPool<PoolableSoundEmitter>(
            defaultCapacity: _initialSize,
            maxPoolSize: _maxPoolSize,
            () => Instantiate(_soundEmitterPrefab)
         );
    }

	private void OnEnable()
	{
		_SFXEventChannel.OnAudioCuePlayRequested += PlayAudioCue;
		_SFXEventChannel.OnAudioCueStopRequested += StopAudioCue;
		_SFXEventChannel.OnAudioCueFinishRequested += FinishAudioCue;

		_musicEventChannel.OnAudioCuePlayRequested += PlayMusicTrack;
		_musicEventChannel.OnAudioCueStopRequested += StopMusic;

		_masterVolumeEventChannel.OnEventRaised += ChangeMasterVolume;
		_musicVolumeEventChannel.OnEventRaised += ChangeMusicVolume;
		_SFXVolumeEventChannel.OnEventRaised += ChangeSFXVolume;

	}

	private void OnDestroy()
	{
		_SFXEventChannel.OnAudioCuePlayRequested -= PlayAudioCue;
		_SFXEventChannel.OnAudioCueStopRequested -= StopAudioCue;

		_SFXEventChannel.OnAudioCueFinishRequested -= FinishAudioCue;
		_musicEventChannel.OnAudioCuePlayRequested -= PlayMusicTrack;

		_musicVolumeEventChannel.OnEventRaised -= ChangeMusicVolume;
		_SFXVolumeEventChannel.OnEventRaised -= ChangeSFXVolume;
		_masterVolumeEventChannel.OnEventRaised -= ChangeMasterVolume;
	}

	/// <summary>
	/// This is only used in the Editor, to debug volumes.
	/// It is called when any of the variables is changed, and will directly change the value of the volumes on the AudioMixer.
	/// </summary>
	void OnValidate()
	{
		if (Application.isPlaying)
		{
			SetGroupVolume("MasterVolume", _masterVolume);
			SetGroupVolume("MusicVolume", _musicVolume);
			SetGroupVolume("SFXVolume", _sfxVolume);
		}
	}
	void ChangeMasterVolume(float newVolume)
	{
		_masterVolume = newVolume;
		SetGroupVolume("MasterVolume", _masterVolume);
	}
	void ChangeMusicVolume(float newVolume)
	{
		_musicVolume = newVolume;
		SetGroupVolume("MusicVolume", _musicVolume);
	}
	void ChangeSFXVolume(float newVolume)
	{
		_sfxVolume = newVolume;
		SetGroupVolume("SFXVolume", _sfxVolume);
	}
	public void SetGroupVolume(string parameterName, float normalizedVolume)
	{
		bool volumeSet = audioMixer.SetFloat(parameterName, NormalizedToMixerValue(normalizedVolume));
		if (!volumeSet)
			Debug.LogError("The AudioMixer parameter was not found");
	}

	public float GetGroupVolume(string parameterName)
	{
		if (audioMixer.GetFloat(parameterName, out float rawVolume))
		{
			return MixerValueToNormalized(rawVolume);
		}
		else
		{
			Debug.LogError("The AudioMixer parameter was not found");
			return 0f;
		}
	}

	// Both MixerValueNormalized and NormalizedToMixerValue functions are used for easier transformations
	/// when using UI sliders normalized format
	private float MixerValueToNormalized(float mixerValue)
	{
		// We're assuming the range [-80dB to 0dB] becomes [0 to 1]
		return 1f + (mixerValue / 80f);
	}
	private float NormalizedToMixerValue(float normalizedValue)
	{
		// We're assuming the range [0 to 1] becomes [-80dB to 0dB]
		// This doesn't allow values over 0dB
		return (normalizedValue - 1f) * 80f;
	}

	private AudioCueKey PlayMusicTrack(AudioCueSO audioCue, AudioConfigurationSO audioConfiguration, Vector3 positionInSpace)
	{
		float fadeDuration = 2f;
		float startTime = 0f;

        bool isFound = _soundEmitterVault.Get(_musicAudioKey, out PoolableSoundEmitter[] soundEmitters);

        if (isFound && soundEmitters[0].Emitter.IsPlaying())
		{
			AudioClip songToPlay = audioCue.GetClips()[0];
			if (soundEmitters[0].Emitter.GetClip() == songToPlay)
				return AudioCueKey.Invalid;

			//Music is already playing, need to fade it out
			startTime = soundEmitters[0].Emitter.FadeMusicOut(fadeDuration);
		}

		var pooledEmitter = _pool.Get();
        pooledEmitter.SetPoolObjectReleaseAction(emitter => {
            var pooledEmitter = emitter as PoolableSoundEmitter;
            _soundEmitterVault.Remove(pooledEmitter.Id);
            _pool.ReturnToPool(pooledEmitter);
        });
        pooledEmitter.Emitter.FadeMusicIn(audioCue.GetClips()[0], audioConfiguration, 1f, startTime);
		_musicAudioKey = _soundEmitterVault.Add(audioCue, new PoolableSoundEmitter[] { pooledEmitter });

        return AudioCueKey.Invalid; //No need to return a valid key for music
	}

	private bool StopMusic(AudioCueKey key)
	{
        bool isFound = _soundEmitterVault.Get(_musicAudioKey, out PoolableSoundEmitter[] soundEmitters);

        if (isFound && soundEmitters[0].Emitter.IsPlaying())
		{
			soundEmitters[0].Emitter.Stop();
			return true;
		}
		else
			return false;
	}

	/// <summary>
	/// Plays an AudioCue by requesting the appropriate number of SoundEmitters from the pool.
	/// </summary>
	public AudioCueKey PlayAudioCue(AudioCueSO audioCue, AudioConfigurationSO settings, Vector3 position = default)
	{
		AudioClip[] clipsToPlay = audioCue.GetClips();
        PoolableSoundEmitter[] soundEmitterArray = new PoolableSoundEmitter[clipsToPlay.Length];

		int nOfClips = clipsToPlay.Length;
		for (int i = 0; i < nOfClips; i++)
		{
            var pooledEmitter = _pool.Get();
            soundEmitterArray[i] = pooledEmitter;
            soundEmitterArray[i].Emitter.PlayAudioClip(clipsToPlay[i], settings, audioCue.looping, position);

            pooledEmitter.SetPoolObjectReleaseAction(emitter => {
				var pooledEmitter = emitter as PoolableSoundEmitter;
				_soundEmitterVault.Remove(pooledEmitter.Id);
				_pool.ReturnToPool(pooledEmitter);
			});
        }

		return _soundEmitterVault.Add(audioCue, soundEmitterArray);
	}

	public bool FinishAudioCue(AudioCueKey audioCueKey)
	{
		bool isFound = _soundEmitterVault.Get(audioCueKey, out PoolableSoundEmitter[] soundEmitters);

		if (isFound)
		{
			for (int i = 0; i < soundEmitters.Length; i++)
			{
				soundEmitters[i].Emitter.Finish();
			}
		}
		else
		{
			Debug.LogWarning("Finishing an AudioCue was requested, but the AudioCue was not found.");
		}

		return isFound;
	}

	public bool StopAudioCue(AudioCueKey audioCueKey)
	{
		bool isFound = _soundEmitterVault.Get(audioCueKey, out PoolableSoundEmitter[] soundEmitters);

		if (isFound)
		{
			for (int i = 0; i < soundEmitters.Length; i++)
			{
                soundEmitters[i].Emitter.Stop();
            }
		}

		return isFound;
	}
}
