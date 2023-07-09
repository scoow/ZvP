using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
   [SerializeField] int _health = 100;
   Animator _animator;
   bool _isDiyng = false;
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    
    void Update()
    {
        Debug.Log(_health);
    }
    public void ProcessHit(int damage)
    {
        _health-= damage;
        if(_health <= 0)
        {
           _isDiyng = true;
           _animator.SetTrigger("IsDiyng");
        }
    }
    public void ZombieDestruction()
    {
         Destroy(gameObject,0.5f);
    }
    public bool IsDiyngOrNot()
    {
        return _isDiyng;
    }
}
