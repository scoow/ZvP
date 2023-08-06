using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioCueSO _music;

    [SerializeField] private AudioCueEventChannelSO _musicEventChannel = default;
    [SerializeField] private AudioConfigurationSO _audioConfig = default;

    private void OnEnable()
    {
        StartCoroutine(PlayMusic());
    }

    private void OnDisable()
    {
        _musicEventChannel?.RaiseStopEvent(AudioCueKey.Invalid);
    }

    private IEnumerator PlayMusic()
    {
        // to be sure that AudioManager is ready
        yield return new WaitForSeconds(1f);
        _musicEventChannel?.RaisePlayEvent(_music, _audioConfig, transform.position);
    }
}
