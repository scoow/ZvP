using UnityEngine;
/// <summary>
/// �������� �������� �������
/// </summary>
public class SoundEventAction : BaseEventAction<AudioClip>
{
    public override void RaiseEventAction(AudioClip sound)
    {
        _baseEventSO.RaiseEvent(sound);
    }
}