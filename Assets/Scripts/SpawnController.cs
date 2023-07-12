using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    // total zombie count to be spawned
    [SerializeField] private int _zombieCount;
    // how much zombies per second
    [SerializeField] private float _spawnRate;

    [SerializeField] private ZombiePoolInteraction _zombie;
    [SerializeField] private Spawner[] _spawners;

    private EventBus _eventBus;

    private float _spawnDelay;
    private float _minDelayOffset;
    private float _maxDelayOffset;

    private ObjPool<ZombiePoolInteraction> _pool;

    private void Awake()
    {
        _spawnDelay = _spawnRate != 0 ? 1 / _spawnRate : 1;
        _minDelayOffset = _spawnDelay * 0.5f * -1;
        _maxDelayOffset = _spawnDelay * 0.5f;

        // TODO: determine proper capacity and size
        _pool = new ObjPool<ZombiePoolInteraction>(
            defaultCapacity: 10,
            maxPoolSize: 40,
            () => Instantiate(_zombie)
         );
    }
    private void Start()
    {
        _eventBus = FindObjectOfType<EventBus>();
    }
    private void Update()
    {
        if (_zombieCount == 0) { return; }

        _spawnDelay -= Time.deltaTime;
        if (_spawnDelay <= 0)
        {
            _spawnDelay = _spawnRate != 0 ? 1 / _spawnRate : 1;
            _spawnDelay += Random.Range(_minDelayOffset, _maxDelayOffset);
            Spawn();
        }
    }

    private void Spawn()
    {
        _zombieCount -= 1;

        var zombie = _pool.Get();
        zombie.SetPoolObjectReleaseAction(
            zombie => StartCoroutine(WaitForDeadStateAnimation(zombie as ZombiePoolInteraction))
        );

        var spawnerIndex = Random.Range(0, _spawners.Length);
        var spawner = _spawners[spawnerIndex];

        zombie.transform.parent = spawner.transform;
        zombie.transform.position = spawner.GetSpawnPosition();

        _eventBus.PostNotification(GameEventType.zombieSpawned, null);
    }

    private IEnumerator WaitForDeadStateAnimation(ZombiePoolInteraction zombie)
    {
        yield return new WaitForSeconds(2f);
        _pool.ReturnToPool(zombie);
    }
}
