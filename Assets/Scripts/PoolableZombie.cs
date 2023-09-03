using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoolableZombie : MonoBehaviour, IPoolable
{
    private Action<IPoolable> _poolReleaseAction;
    private ZombieHealth _health;
    private ZombieMove _move;
    private UpgradeReceiver _upgradeReceiver;

    void Awake()
    {
        _health = GetComponent<ZombieHealth>();
        _move = GetComponent<ZombieMove>();
        _upgradeReceiver = GetComponent<UpgradeReceiver>();
    }

    private void OnEnable()
    {
        _health.dyingEvent.AddListener(ReleaseToPool);
    }

    private void OnDisable()
    {
        _health.dyingEvent.RemoveListener(ReleaseToPool);
    }

    public void SetPoolObjectReleaseAction(Action<IPoolable> action)
    {
        _poolReleaseAction = action;
    }

    public void ResetObjectStateToDefaults()
    {
        _move.ResetState();
        _health.ResetHealth();
        _upgradeReceiver.Reset();
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public void Destroy()
    {
        _health.dyingEvent.RemoveListener(ReleaseToPool);
        Destroy(this);
    }

    private void ReleaseToPool()
    {
        if (_poolReleaseAction == null)
        {
            throw new NullReferenceException($"Release action is not set for poolable object {this.GetType().Name}");
        }
        _poolReleaseAction(this);
    }
}
