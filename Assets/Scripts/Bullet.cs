using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{   
     [SerializeField] float _speed = 1f;
    [SerializeField]  int  _damage = 50;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * _speed * Time.deltaTime);
    }
     
     private void OnTriggerEnter2D(Collider2D other) 
     {
        if(other.gameObject.layer == LayerMask.NameToLayer("Zombie") )
        {
            var _zombieHealth = other.gameObject.GetComponent<ZombieHealth>();
           _zombieHealth.ProcessHit(_damage);
           Destroy(gameObject);
        }
     }
}
