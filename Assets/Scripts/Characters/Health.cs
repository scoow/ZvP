using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private int _fullHealth = 100;
    private int _currentHealth = 0;

    private Animator _animator;
    private Collider2D _collider;

    [SerializeField] public UnityEvent dyingEvent;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();

        if (dyingEvent == null)
            dyingEvent = new UnityEvent();

        ResetHealth();
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
        dyingEvent?.Invoke();
        _animator.SetTrigger("IsDying");
    }

    public void ResetHealth()
    {
        _currentHealth = _fullHealth;
        _collider.enabled = true;
    }
}
