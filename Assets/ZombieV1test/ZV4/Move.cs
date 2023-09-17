using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] int _speed = 3;
    Rigidbody2D rigidbody;  
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
        // transform.Translate(transform.position * _speed * Vector2.left);
        rigidbody.velocity = new Vector2(_speed, 0);
    }
}
