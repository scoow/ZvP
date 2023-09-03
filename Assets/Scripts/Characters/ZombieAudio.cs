using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAudio : CharacterAudio
{
    [SerializeField] private AudioCueSO _zombieSpawned;
    [SerializeField] private AudioCueSO _zombieAtack;
    [SerializeField] private AudioCueSO _zombieDie;

    public void PlaySpawnedSound() => PlayAudio(_zombieSpawned, _audioConfig, transform.position);
    public void PlayAtackSound() => PlayAudio(_zombieAtack, _audioConfig, transform.position);
    public void PlayDeathSound() => PlayAudio(_zombieDie, _audioConfig, transform.position);

    private ZombieHealth _health;

    private void Awake()
    {
        _health = GetComponent<ZombieHealth>();
    }

    private void OnEnable()
    {
        _health.dyingEvent.AddListener(PlayDeathSound);
    }

    private void OnDisable()
    {
        _health.dyingEvent.RemoveListener(PlayDeathSound);
    }

    private void Start()
    {
        PlaySpawnedSound();
    }
}
