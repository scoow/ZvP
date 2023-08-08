using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Listens laneEmpty events on plants Lane and toggles IsAttacking flag for shooting animation
/// </summary>
public class PlantShootingWeapon : MonoBehaviour
{
    private Animator _animator;
    private Lane _lane;

    void Start()
    {
        _animator = GetComponentInParent<Animator>();

        _lane = GetComponentInParent<Lane>();
        _lane?.laneEmptyEvent.AddListener(OnLaneEmpty);

        if (_lane != null && _lane.HasZombies)
        {
            _animator.SetBool("IsAttacking", true);
        }
    }

    private void OnLaneEmpty(bool empty)
    {
        _animator.SetBool("IsAttacking", !empty);
    }

    private void OnDisable()
    {
        _lane?.laneEmptyEvent.RemoveListener(OnLaneEmpty);
    }
}
