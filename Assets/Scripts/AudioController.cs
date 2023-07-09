using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class AudioController : MonoBehaviour
{
    [SerializeField]
    private AudioSource _musicAudioSource;
    [SerializeField]
    private AudioSource _soundAudioSource;
    [SerializeField]
    private AudioClip _zombieSpawnedSound;
    [SerializeField]
    private AudioClip _zombieDiedSound;
    [SerializeField]
    private AudioClip _plantShootedSound;

    public void PlaySound(AudioClip audioClip)
    {
        _soundAudioSource.PlayOneShot(audioClip);
    }
    public void PlayMusic(AudioClip audioClip)
    {
        _musicAudioSource.PlayOneShot(audioClip);
    }

}