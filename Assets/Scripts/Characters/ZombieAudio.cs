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
}
