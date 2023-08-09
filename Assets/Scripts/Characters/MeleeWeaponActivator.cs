using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponActivator : MonoBehaviour
{
    private Animator _animator;
    private Collider2D _collider;

    private float _timer;
    [SerializeField] private float _cooldown = 0.5f;
    [SerializeField] private float _range = 0.5f;

    private RaycastHit2D[] castResults;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();

        _timer = _cooldown;
        castResults = new RaycastHit2D[3];
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer < 0)
        {
            _timer = _cooldown;
            int hitCount = _collider.Cast(Vector2.right, castResults, _range, true);

            bool isZombieDetected = false;
            for (int i = 0; i < hitCount; i++)
            {
                var r = castResults[i];
                if (r.collider != null && r.collider.TryGetComponent<ZombieHealth>(out _))
                {
                    isZombieDetected = true;
                    break;
                }
            }

            _animator.SetBool("IsAttacking", isZombieDetected);
        }
    }

}
