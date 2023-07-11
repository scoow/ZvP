using UnityEngine;
/// <summary>
/// �������� ������� ������������� ����
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class BaseEventAction<T> : MonoBehaviour
{
    [SerializeField] protected BaseEventSO<T> _baseEventSO;
    public abstract void RaiseEventAction(T parameter);
}