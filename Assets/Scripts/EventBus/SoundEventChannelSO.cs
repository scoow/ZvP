using UnityEngine;
/// <summary>
/// ����� ������� ��������������� �����
/// </summary>
[CreateAssetMenu(menuName = "Events/Sound Event Channel")]
public class SoundEventChannelSO : BaseEventSO<AudioClip>
{
    public override void RaiseEvent(AudioClip sound)
    {
        OnEventRaised?.Invoke(sound);
    }
}