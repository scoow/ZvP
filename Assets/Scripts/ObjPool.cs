using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjPool<T> where T: class, IPoolable
{
    private Func<T> _createPoolObjectFunc;
    private IObjectPool<T> _pool;

    public ObjPool(int defaultCapacity, int maxPoolSize, Func<T> createPoolObjectFunc)
    {
        _createPoolObjectFunc = createPoolObjectFunc;

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
        if (_createPoolObjectFunc == null)
        {
            throw new NullReferenceException("Не установлена функция создания элемента ObjPool");
        }
        return _createPoolObjectFunc();
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
