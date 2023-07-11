using System.Collections.Generic;
using UnityEngine;

public class EventBus : MonoBehaviour
{
    /// <summary>
    /// ключ - тип события, значение - список подписанных
    /// </summary>
    private readonly Dictionary<GameEventType, List<IEventListener>> _listeners = new();
    /// <summary>
    /// Добавление подписчика на событие
    /// </summary>
    /// <param name="eventType">тип события</param>
    /// <param name="listener">подписчик</param>
    public void AddListener(GameEventType eventType, IEventListener listener)
    {
        if (_listeners.TryGetValue(eventType, out List<IEventListener> listenList))
        {
            if (!listenList.Contains(listener))
                listenList.Add(listener);
            return;
        }

        listenList = new List<IEventListener>
            {
                listener
            };
        _listeners.Add(eventType, listenList);
    }
    /// <summary>
    /// Удалить одного подписчика
    /// </summary>
    /// <param name="eventType">тип события</param>
    /// <param name="listener">удаляемый подписчик</param>
    public void RemoveListener(GameEventType eventType, IEventListener listener)
    {
        _listeners[eventType]?.Remove(listener);
    }
    /// <summary>
    /// Удалить событие вместе со всеми подписчиками
    /// </summary>
    /// <param name="eventType">тип события</param>
    public void RemoveEvent(GameEventType eventType)
    {
        if (_listeners.TryGetValue(eventType, out List<IEventListener> listenList))
        {
            foreach (IEventListener listener in listenList)
            {
                RemoveListener(eventType, listener);
            }
        }
        _listeners.Remove(eventType);
    }
    /// <summary>
    /// Оповестить всех подписчиков на событие
    /// </summary>
    /// <param name="eventType">тип события</param>
    /// <param name="sender">источник события</param>
    /// <param name="param">дополнительный параметр</param>
    public void PostNotification(GameEventType eventType, Component sender, Object param = null)
    {
        //если нет такого события в словаре
        if (!_listeners.TryGetValue(eventType, out List<IEventListener> listenList))
            return;
        //событие есть
        foreach (IEventListener listener in listenList)
        {
            listener.OnEvent(eventType, sender, param);
        }
    }
}