using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// We should inform Lane when zombie spawns and despawns on that lane
/// so plants with ranged weapon could start/stop shooting.
/// Component should be attached to Zombie
/// </summary>
public class ZombieOnLaneInformer : MonoBehaviour
{
    private Lane _lane;
    private ZombieHealth _zombieHealth;

    private void Awake()
    {
        _zombieHealth = GetComponent<ZombieHealth>();
    }

    private void OnEnable()
    {       
        _zombieHealth.dyingEvent.AddListener(ZombieDied);
    }

    private void OnDisable()
    {
        _zombieHealth.dyingEvent.RemoveListener(ZombieDied);
    }

    private void Start()
    {
        _lane = GetComponentInParent<Lane>();
        _lane?.ZombieSpawned();
    }

    private void ZombieDied()
    {
        _lane?.ZombieDie();
    }
}
