using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Health : MonoBehaviour
{
    [SerializeField] private float _fullHealth = 100f;
    private protected float _currentHealth = 0f;

    private Animator _animator;
    private protected Collider2D _collider;

    [SerializeField] public UnityEvent dyingEvent;

    protected virtual void Awake()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();

        if (dyingEvent == null)
            dyingEvent = new UnityEvent();

        ResetHealth();
    }

    public virtual void ProcessHit(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0f && _collider.enabled)
        {
            Die();
        }
    }

    public void Die()
    {
        _collider.enabled = false;
        dyingEvent?.Invoke();
        _animator.SetTrigger("IsDying");
    }

    public void ResetHealth()
    {
        _currentHealth = _fullHealth;
        _collider.enabled = true;
    }
}
