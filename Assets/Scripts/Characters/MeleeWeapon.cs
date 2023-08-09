using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    [SerializeField] private int _damage = 20;
    [SerializeField] private float _range = 0.5f;
    [SerializeField] private int _targetCountCanHitAtOnce = 1;

    private Collider2D _collider;
    private RaycastHit2D[] castResults;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        castResults = new RaycastHit2D[_targetCountCanHitAtOnce];
    }

    public void Hit()
    {
        int targetCount = _collider.Cast(Vector2.right, castResults, _range, true);

        for (int i = 0; i < targetCount; i++)
        {
            var r = castResults[i];
            if (r.collider != null && r.collider.TryGetComponent<ZombieHealth>(out ZombieHealth zombie))
            {
                zombie.ProcessHit(_damage);
            }
        }       
    }
}
