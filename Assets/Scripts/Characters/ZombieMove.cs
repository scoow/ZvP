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
    private ZombieAudio _audio;

    bool _isAttacking = false;
    bool _isDying = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _zombieHealth = GetComponent<ZombieHealth>();
        _audio = GetComponent<ZombieAudio>();
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
            transform.Translate(_speed * Time.deltaTime * Vector2.left); 
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlantHealth plantHealth))
        {
            _plantHealth = plantHealth;
            if (_plantHealth != null)
            {
                _isAttacking = true;
                _animator.SetBool("IsAttacking", true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Plant"))
        {
            _isAttacking = false;
            _animator.SetBool("IsAttacking", false);
        }
    }

    public void DamagePlant()
    {
        if (_plantHealth == null) return;
        _plantHealth.ProcessHit(_damage);//? не работает с объектами Unity
        _audio.PlayAtackSound();
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
