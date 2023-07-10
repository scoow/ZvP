using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMove : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] int _damage = 50;
    Animator _animator;
    PlantHealth _plantHealth;
    ZombieHealth _zombieHealth;

    bool _isAttacking = false;
    bool _isDying = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _zombieHealth = GetComponent<ZombieHealth>();
    }

    private void OnEnable()
    {
        _zombieHealth.zombieDying.AddListener(ZombieDying);
    }

    private void OnDisable()
    {
        _zombieHealth.zombieDying.RemoveListener(ZombieDying);
    }

    void Update()
    {
        if (!_isAttacking && !_isDying)
        { 
            transform.Translate(Vector2.left * _speed * Time.deltaTime); 
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Plant"))
        {
            _plantHealth = other.gameObject.GetComponent<PlantHealth>();
            if (_plantHealth != null)
            {
                Debug.Log("Атака!!!!");
                _isAttacking = true;
                _animator.SetBool("IsAttacking", true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Plant"))
        {
            Debug.Log("Пошли дальше");
            _isAttacking = false;
            _animator.SetBool("IsAttacking", false);
        }
    }

    private void ProcessHitPlant(PlantHealth _health)
    {
        _health.ProcessHit(_damage);
    }

    public void DamagePlant()
    {
        ProcessHitPlant(_plantHealth);
    }

    public void ZombieDying()
    {
        _isDying = true;
    }

    public void ResetState()
    {
        _isDying = false;
        _isAttacking = false;
    }
}
