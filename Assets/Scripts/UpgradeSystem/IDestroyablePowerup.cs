using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IDestroyablePowerup
{
    void OnPowerupDestroyed(UnityAction onArmorDestroyed);
}
