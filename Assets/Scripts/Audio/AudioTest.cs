using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTest : MonoBehaviour
{
    [Header("Sounds")]
    [SerializeField] private AudioCueSO _music;
    [SerializeField] private AudioCueSO _zombieSpawned;
    [SerializeField] private AudioCueSO _zombieAtack;
    [SerializeField] private AudioCueSO _zombieDie;

    [Header("Sound configs")]
    [SerializeField] private AudioConfigurationSO _audioConfig = default;
    [SerializeField] private AudioConfigurationSO _musicConfig = default;

    [Header("Broadcasting on channels")]
    [SerializeField] private AudioCueEventChannelSO _SFXEventChannel = default;
    [SerializeField] private AudioCueEventChannelSO _musicEventChannel = default;

    //private AudioCueKey _musicKey;

    void Update()
    {
#if UNITY_EDITOR
        // Music
        if (Input.GetKeyUp(KeyCode.Q))
        {
            PlayMusic(_music);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            StopStopMusic();
        }

        // SFX
        if (Input.GetKeyUp(KeyCode.A))
        {
            PlaySFX(_zombieSpawned);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            PlaySFX(_zombieAtack);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            PlaySFX(_zombieDie);
        }
#endif
    }

    private AudioCueKey PlayMusic(AudioCueSO audioCue)
    {
        return _musicEventChannel.RaisePlayEvent(audioCue, _musicConfig);
    }

    private void StopStopMusic()
    {
        _musicEventChannel.RaiseStopEvent(AudioCueKey.Invalid);
    }

    private void PlaySFX(AudioCueSO audioCue)
    {
        _SFXEventChannel.RaisePlayEvent(audioCue, _audioConfig);
    }
}
