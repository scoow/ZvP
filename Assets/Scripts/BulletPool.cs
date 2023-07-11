using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    private ObjPool<BulletPoolInteraction> _pool;

    [SerializeField] private BulletPoolInteraction _bulletPrefab;

    private void Awake()
    {
        // TODO: determine proper capacity and size
        _pool = new ObjPool<BulletPoolInteraction>(
            defaultCapacity: 75,
            maxPoolSize: 200,
            () => Instantiate(_bulletPrefab)
         );
    }

    public void FireBulletFrom(Transform weapon)
    {
        var bullet = _pool.Get();
        bullet.SetPoolObjectReleaseAction(
            bullet => _pool.ReturnToPool(bullet as BulletPoolInteraction)
        );

        bullet.transform.parent = transform;
        bullet.transform.position = weapon.position;
    }
}
