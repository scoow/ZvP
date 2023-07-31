using UnityEngine;

public class LeftWallTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<ZombieHealth>(out ZombieHealth zombieHealth))
            zombieHealth.Die();
    }
}