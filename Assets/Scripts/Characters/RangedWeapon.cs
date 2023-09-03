using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : MonoBehaviour
{
    [SerializeField] Transform _weapon;

    // Same SO as in BulletPool's _fireEventChannel
    [Header("Broadcasting on")]
    [SerializeField] private Vector3EventChannelSO _fireEventChannel;

    public void Fire()
    {
        _fireEventChannel.RaiseEvent(_weapon.position);
    }
}
