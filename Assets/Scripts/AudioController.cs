using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class AudioController : MonoBehaviour
{
    [SerializeField]
    private AudioSource _musicAudioSource;
    [SerializeField]
    private AudioSource _soundAudioSource;
    [SerializeField]
    private SoundEventChannelSO _soundEventChannelSO;

    private void OnEnable()
    {
        if (_soundEventChannelSO != null)
        {
            _soundEventChannelSO.OnEventRaised += context => PlaySound(context);
        }
    }
    private void OnDisable()
    {
        if (_soundEventChannelSO != null)
        {
            _soundEventChannelSO.OnEventRaised -= context => PlaySound(context);
        }
    }
    public void PlaySound(AudioClip audioClip)
    {
        _soundAudioSource.PlayOneShot(audioClip);
    }
    public void PlayMusic(AudioClip audioClip)
    {
        _musicAudioSource.PlayOneShot(audioClip);
    }

}