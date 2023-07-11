using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPoolInteraction : MonoBehaviour, IPoolable
{
    private Action<IPoolable> _poolReleaseAction;
    private Bullet _bullet;

    void Awake()
    {
        _bullet = GetComponent<Bullet>();
    }

    private void OnEnable()
    {
        _bullet.bulletHitTargetEvent.AddListener(OnBulletHitTarget);
    }

    private void OnDisable()
    {
        _bullet.bulletHitTargetEvent.RemoveListener(OnBulletHitTarget);
    }

    public void Destroy()
    {
        _bullet.bulletHitTargetEvent.RemoveListener(OnBulletHitTarget);
        Destroy(this);
    }

    public void ResetObjectStateToDefaults()
    {
        // Bullet has no state
    }

    public void SetActive(bool active)
    {
        gameObject?.SetActive(active);
    }

    public void SetPoolObjectReleaseAction(Action<IPoolable> action)
    {
        _poolReleaseAction = action;
    }

    private void OnBulletHitTarget()
    {
        if (_poolReleaseAction == null)
        {
            throw new NullReferenceException("Ќе установлен делегат дл€ выполнени€ возврата Bullet в пул");
        }
        _poolReleaseAction(this);
    }
}
