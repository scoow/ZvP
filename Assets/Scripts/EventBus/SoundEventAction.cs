using UnityEngine;
/// <summary>
/// Вызывает звуковое событие
/// </summary>
public class SoundEventAction : BaseEventAction<AudioClip>
{
    public override void RaiseEventAction(AudioClip sound)
    {
        _baseEventSO.RaiseEvent(sound);
    }
}