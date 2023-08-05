using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

// This class differs from the original by using Dictionary instead of lists,
// also it uses additional dictionary to hold relations of emitters to AudioCueKey to help remove individual emitters from _emitters
// Original: https://github.com/UnityTechnologies/open-project-1/blob/main/UOP1_Project/Assets/Scripts/Audio/SoundEmitters/SoundEmitterVault.cs

public class SoundEmitterVault
{
    private int _currentKey = 0;

    private Dictionary<AudioCueKey, PoolableSoundEmitter[]> _emitters;
    private Dictionary<Guid, AudioCueKey> _poolableEmittersToKey;

    public SoundEmitterVault()
    {
        _emitters = new Dictionary<AudioCueKey, PoolableSoundEmitter[]>();
        _poolableEmittersToKey = new Dictionary<Guid, AudioCueKey>();
    }

    public AudioCueKey GetKey(AudioCueSO cue)
    {
        return new AudioCueKey(_currentKey++, cue);
    }

    public void Add(AudioCueKey key, PoolableSoundEmitter[] emitters)
    {
        _emitters.Add(key, emitters);

        foreach (var emitter in emitters)
        {
            _poolableEmittersToKey[emitter.Id] = key;
        }
    }

    public AudioCueKey Add(AudioCueSO cue, PoolableSoundEmitter[] emitters)
    {
        AudioCueKey emitterKey = GetKey(cue);
        _emitters.Add(emitterKey, emitters);

        foreach (var emitter in emitters)
        {
            _poolableEmittersToKey[emitter.Id] = emitterKey;
        }

        return emitterKey;
    }

    public bool Get(AudioCueKey key, out PoolableSoundEmitter[] emitter)
    {
        if (key == AudioCueKey.Invalid)
        {
            emitter = null;
            return false;
        }

        return _emitters.TryGetValue(key, out emitter);
    }

    public bool Remove(AudioCueKey key)
    {
        if (_emitters.ContainsKey(key))
        {
            // remove entries from dictionaries
            var emitters = _emitters[key];
            _emitters.Remove(key);
            foreach (var emitter in emitters)
            {
                _poolableEmittersToKey.Remove(emitter.Id);
            }

            return true;
        }

        return false;
    }

    public bool Remove(Guid pooledEmitterId)
    {
        if (_poolableEmittersToKey.ContainsKey(pooledEmitterId))
        {
            var key = _poolableEmittersToKey[pooledEmitterId];
            _poolableEmittersToKey.Remove(pooledEmitterId);

            if (_emitters.ContainsKey(key))
            {
                // if there is still emitters left with same AudioCueKey
                // remove that emitter from array in _emitters
                var emitters = _emitters[key];
                if (emitters.Length > 1)
                {
                    PoolableSoundEmitter[] newArray = emitters.Where(element => element.Id != pooledEmitterId).ToArray();
                    _emitters[key] = newArray;
                }
                else
                {
                    // remove all (actually last) emitters from _emitters
                    _emitters.Remove(key);
                }
            }

            return true;
        }

        return false;
    }
}
