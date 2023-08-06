using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlantSooter : MonoBehaviour
{
    [SerializeField] Transform _weapon;

    private BulletPool _bulletPool;

    private void Awake()
    {
        _bulletPool = FindObjectOfType<BulletPool>();
    }

    public void Fire()
    {
        _bulletPool.FireBulletFrom(_weapon);
    }
}
