using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class AudioController : MonoBehaviour, IEventListener
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
    private AudioClip _plantSpawnedSound;
    [SerializeField]
    private AudioClip _plantDiedSound;
    [SerializeField]
    private AudioClip _plantShootedSound;

    private void PlaySound(AudioClip audioClip)
    {
        _soundAudioSource.PlayOneShot(audioClip);
    }
    private void PlayMusic(AudioClip audioClip)
    {
        _musicAudioSource.PlayOneShot(audioClip);
    }

    public void OnEvent(GameEventType eventType, Component sender, Object param = null)
    {
        switch (eventType)
        {
            case GameEventType.zombieSpawned:
                PlaySound(_zombieSpawnedSound);
                break;
            case GameEventType.zombieDied:
                PlaySound(_zombieDiedSound);
                break;
            case GameEventType.plantSpawned:
                PlaySound(_plantSpawnedSound);
                break;
            case GameEventType.plantDied:
                PlaySound(_plantDiedSound);
                break;
            case GameEventType.plantShooted:
                PlaySound(_plantShootedSound);
                break;
            default:
                break;
        }
    }
}