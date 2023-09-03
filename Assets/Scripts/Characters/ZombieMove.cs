using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ZombieMove : MonoBehaviour
{
    [SerializeField] private float _initialSpeed;
    private float _currentSpeed;
    [SerializeField] int _damage = 50;

    private Animator _animator;
    private PlantHealth _plantHealth;
    private ZombieHealth _zombieHealth;
    private ZombieAudio _audio;
    private UpgradeReceiver _upgradeReceiver;

    bool _isAttacking = false;
    bool _isDying = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _zombieHealth = GetComponent<ZombieHealth>();
        _audio = GetComponent<ZombieAudio>();
        _upgradeReceiver = GetComponent<UpgradeReceiver>();

        ResetState();
    }

    private void OnEnable()
    {
        _zombieHealth.dyingEvent.AddListener(ZombieDying);
        _upgradeReceiver.upgradeAppliedEvent.AddListener(OnUpgradeApplied);
    }

    private void OnDisable()
    {
        _zombieHealth.dyingEvent.RemoveListener(ZombieDying);
        _upgradeReceiver.upgradeAppliedEvent.RemoveListener(OnUpgradeApplied);
    }

    void Update()
    {
        if (!_isAttacking && !_isDying)
        {
            transform.Translate(_currentSpeed * Time.deltaTime * Vector2.left);
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

    // TODO: Remove attack functionality from that component
    public void DamagePlant()
    {
        if (_plantHealth == null) return;
        _plantHealth.ProcessHit(_damage);
        _audio.PlayAtackSound();
    }

    public void ZombieDying()
    {
        _isDying = true;
    }

    private void OnUpgradeApplied(IEnumerable<UpgradeableAttribute> modifiedAttributes)
    {
        if (modifiedAttributes.Contains(UpgradeableAttribute.MovementSpeed))
        {
            _currentSpeed = _upgradeReceiver.ProcessValue(UpgradeableAttribute.MovementSpeed, _initialSpeed);
        }
    }

    public void ResetState()
    {
        _isDying = false;
        _isAttacking = false;
        _currentSpeed = _initialSpeed;
    }
}
