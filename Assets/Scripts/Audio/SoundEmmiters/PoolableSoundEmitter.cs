using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SoundEmitter))]
public class PoolableSoundEmitter : MonoBehaviour, IPoolable
{
    public SoundEmitter Emitter { get; private set; }
    private Action<IPoolable> _poolReleaseAction;

    public Guid Id { get; private set; }

    void Awake()
    {
        Emitter = GetComponent<SoundEmitter>();
        Id = Guid.NewGuid();
    }

    private void OnEnable()
    {
        Emitter.OnSoundFinishedPlaying += OnSoundFinishedPlaying;
    }

    private void OnDisable()
    {
        Emitter.OnSoundFinishedPlaying -= OnSoundFinishedPlaying;
    }

    public void Destroy()
    {
        Destroy(this);
    }

    public void ResetObjectStateToDefaults()
    {
        // SoundEmitter has state?
    }

    public void SetActive(bool active)
    {
        gameObject?.SetActive(active);
    }

    public void SetPoolObjectReleaseAction(Action<IPoolable> action)
    {
        _poolReleaseAction = action;
    }

    private void OnSoundFinishedPlaying()
    {
        if (_poolReleaseAction == null)
        {
            throw new NullReferenceException($"Release action is not set for poolable object {this.GetType().Name}");
        }
        _poolReleaseAction(this);
    }
}
