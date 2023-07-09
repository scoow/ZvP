using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantHealth : MonoBehaviour
{
    [SerializeField] int _health = 100;
    void Start()
    {
        
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
            Destroy(gameObject);
        }
    }
}
