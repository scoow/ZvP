using UnityEngine;

public interface IEventListener
{
    void OnEvent(GameEventType eventType, Component sender, Object param = null);
}