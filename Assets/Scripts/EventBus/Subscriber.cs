using System;
using UnityEngine;

public class Subscriber : MonoBehaviour
{
    private EventBus _eventBus;

    private AudioController _audioController;

    void Start()
    {
        _audioController = FindObjectOfType<AudioController>();
        _eventBus = FindObjectOfType<EventBus>();

        Subscribe();
    }

    private void Subscribe()
    {
        _eventBus.AddListener(GameEventType.zombieSpawned, _audioController);
        _eventBus.AddListener(GameEventType.zombieDied, _audioController);
        _eventBus.AddListener(GameEventType.plantSpawned, _audioController);
        _eventBus.AddListener(GameEventType.plantDied, _audioController);
        _eventBus.AddListener(GameEventType.plantShooted, _audioController);
    }
}