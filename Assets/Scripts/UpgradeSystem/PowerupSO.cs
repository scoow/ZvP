using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents changes to value that is related to one Attribute.
/// 
/// i.g. armor reduces damage - armor here is UpgradeItem that has powerup that reduces value of received damage.
/// </summary>
public abstract class PowerupSO: ScriptableObject
{
    // property should be used to distinguish powerups that affects different attributes
    [SerializeField] public Attribute AppliesTo { get; private set; }

    /// <summary>
    /// Changes <paramref name="initialValue"/> to reflect effect of powerup
    /// </summary>
    /// <param name="initialValue">value that should be changed</param>
    /// <returns>value after changes</returns>
    public abstract float Modify(float initialValue);
}
