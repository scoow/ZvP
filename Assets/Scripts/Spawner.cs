using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _zombie;

    public void Spawn()
    {
        var offset = Random.Range(0, 5);
        var pos = transform.parent.position;
        var spawnPosition = new Vector3(pos.x, pos.y + offset * 0.1f, pos.z);
        Instantiate(_zombie, spawnPosition, Quaternion.identity);
    }
}
