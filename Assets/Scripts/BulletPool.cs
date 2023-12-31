using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    private ObjPool<PoolableBullet> _pool;

    [SerializeField] private PoolableBullet _bulletPrefab;
    [Header("Listening on")]
    [SerializeField] private Vector3EventChannelSO _fireEventChannel;

    private void Awake()
    {
        // TODO: determine proper capacity and size
        _pool = new ObjPool<PoolableBullet>(
            defaultCapacity: 75,
            maxPoolSize: 200,
            () => Instantiate(_bulletPrefab)
         );
    }

    private void OnEnable()
    {
        _fireEventChannel.OnEventRaised += FireBulletFrom;
    }

    private void OnDisable()
    {
        _fireEventChannel.OnEventRaised -= FireBulletFrom;
    }

    private void FireBulletFrom(Vector3 weaponPosition)
    {
        var bullet = _pool.Get();
        bullet.SetPoolObjectReleaseAction(
            bullet => _pool.ReturnToPool(bullet as PoolableBullet)
        );

        bullet.transform.parent = transform;
        bullet.transform.position = weaponPosition;
    }
}
