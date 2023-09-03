using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Upgrades/Upgrade item")]
public class UpgradeItemSO : ScriptableObject
{
    // UpgradeItem can be applied only to a particular body part
    public BodyPart AppliesTo = BodyPart.Nothing;

    [SerializeField] private PowerupBaseSO[] _powerups = default;

    public IEnumerable<PowerupBaseSO> GetPowerupsFor(UpgradeableAttribute attribute)
    {
        return _powerups.Where(powerup => powerup.Modifies == attribute);
    }

    /// <summary>
    /// To know wich UpgradeableAttributes are modified
    /// </summary>
    /// <returns>IEnumerable of distinct UpgradeableAttributes that UpgradeItemSO has in its powerups</returns>
    public IEnumerable<UpgradeableAttribute> GetModifiedAttributes()
    {
        return from p in _powerups
               group p by p.Modifies into g
               select g.Key;
    }

    public void SetOnDestroyAction(UnityAction onDestroy)
    {
        var destroyables = _powerups.Where(powerup => powerup is IDestroyablePowerup);
        foreach (var destroyable in destroyables)
        {
            (destroyable as IDestroyablePowerup).OnPowerupDestroyed(onDestroy);
        }
    }

    public UpgradeItemSO Clone()
    {
        var clone = Instantiate(this);
        for (int i = 0;  i < _powerups.Length; i++)
        {
            clone._powerups[i] = Instantiate(_powerups[i]);
        }

        return clone;
    }
}
