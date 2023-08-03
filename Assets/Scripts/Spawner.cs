using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Vector3 GetSpawnPosition()
    {
        var offset = Random.Range(0, 5);
        var pos = transform.position;
        return new Vector3(pos.x, pos.y /*+ offset * 0.1f*/, pos.z);
    }
}
