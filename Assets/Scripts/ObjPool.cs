using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjPool<T> where T: class, IPoolable
{
    private Func<T> _createPoolObjectAction;
    private IObjectPool<T> _pool;

    public ObjPool(int defaultCapacity, int maxPoolSize, Func<T> createPoolObjectAction)
    {
        _createPoolObjectAction = createPoolObjectAction;

        _pool = new ObjectPool<T>(
                        CreatePooledItem,
                        OnTakeFromPool,
                        OnReturnedToPool,
                        OnDestroyPoolObject,
                        true,
                        defaultCapacity,
                        maxPoolSize);
    }

    public T Get() 
    {
        return _pool.Get();
    }

    public void ReturnToPool(T obj)
    {
        _pool.Release(obj);
    }

    private T CreatePooledItem()
    {
        if (_createPoolObjectAction == null)
        {
            throw new NullReferenceException("Не установлена функция создания элемента ObjPool");
        }
        return _createPoolObjectAction();
    }

    private void OnReturnedToPool(T poolObject)
    {
        poolObject.SetActive(false);
    }

    private void OnTakeFromPool(T poolObject)
    {
        poolObject.ResetObjectStateToDefaults();
        poolObject.SetActive(true);
    }

    private void OnDestroyPoolObject(T poolObject)
    {
        poolObject.Destroy();
    }
}
