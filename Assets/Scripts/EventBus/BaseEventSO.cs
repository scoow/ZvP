using UnityEngine.Events;

/// <summary>
/// ������� ����������� ����� �������� �������.
/// ������ ��� ����� ����� ����������� ������� � ������������.
/// </summary>
/// <typeparam name="T">��� ������������� ���������</typeparam>

public abstract class BaseEventSO<T> : SerializableScriptableObject
{
    public UnityAction<T> OnEventRaised;

    public abstract void RaiseEvent(T item);
}