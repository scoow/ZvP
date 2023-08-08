using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Helps shooting plants to know if their lane has zombies and they should actually shoot them
/// </summary>
public class Lane : MonoBehaviour
{
    private int _zombiesOnLaneCount;
    public bool HasZombies => _zombiesOnLaneCount > 0;

    // fires false when zombie spawns on lane, when lane becomes empty - true
    public UnityEvent<bool> laneEmptyEvent;

    private void Start()
    {
        if (laneEmptyEvent == null)
            laneEmptyEvent = new UnityEvent<bool>();
    }

    /// <summary>
    /// Called by zombie when he spawns on lane.
    /// Zombie should be child of that lane.
    /// </summary>
    public void ZombieSpawned()
    {
        _zombiesOnLaneCount++;
        if (laneEmptyEvent != null && _zombiesOnLaneCount == 1)
        {
            laneEmptyEvent.Invoke(false);
        }
    }

    /// <summary>
    /// Called by zombie when he dies on lane.
    /// Zombie should be child of that lane.
    /// </summary>
    public void ZombieDie()
    {
        _zombiesOnLaneCount--;
        if (laneEmptyEvent != null && _zombiesOnLaneCount == 0)
        {
            laneEmptyEvent.Invoke(true);
        }
    }
}
