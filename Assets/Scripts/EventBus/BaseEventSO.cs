using UnityEngine.Events;

/// <summary>
/// Базовый абстрактный класс игрового события.
/// Служит для связи между источниками событий и подписчиками.
/// </summary>
/// <typeparam name="T">Тип передаваемого аргумента</typeparam>

public abstract class BaseEventSO<T> : SerializableScriptableObject
{
    public UnityAction<T> OnEventRaised;

    public abstract void RaiseEvent(T item);
}