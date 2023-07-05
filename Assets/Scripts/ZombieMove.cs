using UnityEngine;

public class ZombieMove : MonoBehaviour
{
    [SerializeField] private float _speed;
    void Update()
    {
/*        Vector2 position = transform.position;
        position.x*/
        transform.Translate(Vector2.left * _speed * Time.deltaTime);
    }
}
