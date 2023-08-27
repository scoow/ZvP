using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradeItemSO : ScriptableObject
{
    // UpgradeItem can be applied only to a particular body part
    [SerializeField] public BodyPart AppliesTo { get; private set; }

    [SerializeField] private PowerupSO[] _powerups = default;
    
    public IEnumerable<PowerupSO> GetPowerupsFor(Attribute attribute)
    {
        return _powerups.Where(powerup => powerup.AppliesTo == attribute);
    }
}
