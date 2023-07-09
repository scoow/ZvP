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

    private void Start() 
    {
        _animator = GetComponent<Animator>();
        _zombieHealth = GetComponent<ZombieHealth>();
        
    }
    void Update()
    {
/*        Vector2 position = transform.position;
        position.x*/
       if(!_isAttacking && !_zombieHealth.IsDiyngOrNot())
       { transform.Translate(Vector2.left * _speed * Time.deltaTime);}
    } 
     private void OnTriggerEnter2D(Collider2D other) 
    {
       if(other.gameObject.layer == LayerMask.NameToLayer("Plant") )
          {   _plantHealth = other.gameObject.GetComponent<PlantHealth>();
             if(_plantHealth!=null)
            { Debug.Log("Атака!!!!");
            _isAttacking = true;
            _animator.SetBool("IsAttacking",true);   
               }  }   
    }
    private void OnTriggerExit2D(Collider2D other) 
    {
          if(other.gameObject.layer == LayerMask.NameToLayer("Plant") )
          {   _plantHealth = other.gameObject.GetComponent<PlantHealth>();
           
             Debug.Log("Пошли дальше");
            _isAttacking = false;
            _animator.SetBool("IsAttacking",false);   
          }   
    }

    private void ProcessHitPlant(PlantHealth  _health)
    {
        _health.ProcessHit(_damage);

    }
    public void DamagePlant()
    {
             ProcessHitPlant(_plantHealth);
    }
         
}
