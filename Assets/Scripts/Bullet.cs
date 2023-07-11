using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{
    [SerializeField] float _speed = 1f;
    [SerializeField] int _damage = 50;

    [SerializeField] public UnityEvent bulletHitTargetEvent;

    [SerializeField] private float rightBorder = 30;

    void Start()
    {
        if (bulletHitTargetEvent == null)
            bulletHitTargetEvent = new UnityEvent();
    }

    void Update()
    {
        transform.Translate(Vector2.right * _speed * Time.deltaTime);
        if (transform.position.x > rightBorder)
        {
            if (bulletHitTargetEvent != null)
                bulletHitTargetEvent.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out ZombieHealth zombieHealth))
        {
            zombieHealth.ProcessHit(_damage);
            if (bulletHitTargetEvent != null)
                bulletHitTargetEvent.Invoke();
        }
    }
}
