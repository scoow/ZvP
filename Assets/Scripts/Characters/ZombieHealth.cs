using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ZombieHealth : MonoBehaviour
{
    [SerializeField] private int _fullHealth = 100;
    private int _currentHealth = 0;

    private Animator _animator;
    private Collider2D _collider;
    private ZombieAudio _audio;

    [SerializeField] public UnityEvent zombieDying;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();
        _audio = GetComponent<ZombieAudio>();

        _collider.enabled = true;
        _currentHealth = _fullHealth;
    }

    void Start()
    {
        if (zombieDying == null)
            zombieDying = new UnityEvent();

        _audio.PlaySpawnedSound();
    }

    public void ProcessHit(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0 && _collider.enabled)
        {
            Die();
        }
    }

    public void Die()
    {
        _collider.enabled = false;
        zombieDying?.Invoke();
        _animator.SetTrigger("IsDiyng");
        _audio.PlayDeathSound();
    }

    public void ResetHealth() 
    {
        _currentHealth = _fullHealth;
        _collider.enabled = true;
    }
}