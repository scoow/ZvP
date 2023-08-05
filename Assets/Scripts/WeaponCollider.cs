using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    Animator _animator;
    ZombieHealth _zombieHealth;
    void Start()
    {
        _animator = GetComponentInParent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out ZombieHealth zombieHealth))
        {
            _zombieHealth = zombieHealth;
            if (_zombieHealth != null)
            {
                _animator.SetBool("IsAttacking", true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out ZombieHealth zombieHealth))
        {
            _zombieHealth = zombieHealth;
            _animator.SetBool("IsAttacking", false);
        }
    }


}
