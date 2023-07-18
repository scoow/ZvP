using System.Collections.Generic;
using UnityEngine;

public class EventBus : MonoBehaviour
{
    /// <summary>
    /// ���� - ��� �������, �������� - ������ �����������
    /// </summary>
    private readonly Dictionary<GameEventType, List<IEventListener>> _listeners = new();
    /// <summary>
    /// ���������� ���������� �� �������
    /// </summary>
    /// <param name="eventType">��� �������</param>
    /// <param name="listener">���������</param>
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
    /// ������� ������ ����������
    /// </summary>
    /// <param name="eventType">��� �������</param>
    /// <param name="listener">��������� ���������</param>
    public void RemoveListener(GameEventType eventType, IEventListener listener)
    {
        _listeners[eventType]?.Remove(listener);
    }
    /// <summary>
    /// ������� ������� ������ �� ����� ������������
    /// </summary>
    /// <param name="eventType">��� �������</param>
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
    /// ���������� ���� ����������� �� �������
    /// </summary>
    /// <param name="eventType">��� �������</param>
    /// <param name="sender">�������� �������</param>
    /// <param name="param">�������������� ��������</param>
    public void PostNotification(GameEventType eventType, Component sender, Object param = null)
    {
        //���� ��� ������ ������� � �������
        if (!_listeners.TryGetValue(eventType, out List<IEventListener> listenList))
            return;
        //������� ����
        foreach (IEventListener listener in listenList)
        {
            listener.OnEvent(eventType, sender, param);
        }
    }
}