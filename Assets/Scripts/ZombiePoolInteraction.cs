using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ZombiePoolInteraction : MonoBehaviour, IPoolable
{
    private Action<IPoolable> _poolReleaseAction;
    private ZombieHealth _health;
    private ZombieMove _move;

    void Awake()
    {
        _health = GetComponent<ZombieHealth>();
        _move = GetComponent<ZombieMove>();
    }

    private void OnEnable()
    {
        _health.zombieDying.AddListener(ReleaseToPool);
    }

    private void OnDisable()
    {
        _health.zombieDying.RemoveListener(ReleaseToPool);
    }

    public void SetPoolObjectReleaseAction(Action<IPoolable> action)
    {
        _poolReleaseAction = action;
    }

    public void ResetObjectStateToDefaults()
    {
        _move.ResetState();
        _health.ResetHealth();
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public void Destroy()
    {
        Destroy(this);
    }

    private void ReleaseToPool()
    {
        if (_poolReleaseAction == null)
        {
            throw new NullReferenceException("Не установлен делегат для выполнения возврата элемента в пул");
        }
        _poolReleaseAction(this);
    }
}
