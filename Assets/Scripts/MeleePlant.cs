using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleePlant : MonoBehaviour
{
    [SerializeField] int _damage = 50;
    Animator _animator;
    PlantHealth _plantHealth;
    ZombieHealth _zombieHealth;

    void Start()
    {
        _plantHealth = GetComponent<PlantHealth>();
        _animator = GetComponent<Animator>();
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
    public void DamageZombie()
    {
        _zombieHealth.ProcessHit(_damage);
    }

}
