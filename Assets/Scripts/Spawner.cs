using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float _spawnRate;
    [SerializeField] private GameObject _zombie;
    private float _spawnDelay;
    private float _timer;

    private void Awake()
    {
        _spawnDelay = _spawnRate != 0 ?1 / _spawnRate : 1;
        _timer = _spawnDelay;
    }
    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer < 0)
        {
            _timer = _spawnDelay;
            Spawn();
        }
    }

    private void Spawn()
    {
        Instantiate(_zombie);
        Debug.Log("Spawn");
    }
}
