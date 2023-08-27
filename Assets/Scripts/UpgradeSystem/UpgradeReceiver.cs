using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Should be added as component to upgradeable GameObject (zombie).
/// Manages upgrades to each body part. No more than one upgrade could be applied to one body part.
/// </summary>
public class UpgradeReceiver : MonoBehaviour
{
    private Dictionary<BodyPart, UpgradeItemSO> _upgrades = default;

    /// <summary>
    /// Registers UpgradeItemSO as upgrade to a particular budy part
    /// </summary>
    /// <param name="upgrade"></param>
    /// <returns>false, if upgrade couldn't be applied</returns>
    public bool Apply(UpgradeItemSO upgrade)
    {
        if (_upgrades.ContainsKey(upgrade.AppliesTo))
        {
            return false;
        }

        _upgrades[upgrade.AppliesTo] = upgrade;
        return true;
    }

    /// <summary>
    /// Modifies <paramref name="initialValue"/> with cumulative effect of each powerup that can be applied to <paramref name="attribute"/>
    /// </summary>
    /// <param name="attribute">powerups are filtered by this attribute value</param>
    /// <param name="initialValue">value before applying powerup modifiers</param>
    /// <returns>value after changes</returns>
    public float ProcessValue(Attribute attribute, float initialValue)
    {
        float result = initialValue;
        foreach (var upgrade in _upgrades)
        {
            var powerups = upgrade.Value.GetPowerupsFor(attribute);
            result = ApplyModifiersToValue(powerups, result);
        }
        return result;
    }

    private float ApplyModifiersToValue(IEnumerable<PowerupSO> powerups, float initialValue)
    {
        float result = initialValue;
        foreach (var powerup in powerups)
        {
            result = powerup.Modify(initialValue);
        }
        return result;
    }

    /// <summary>
    /// Should be called before releasing to object pool
    /// </summary>
    public void Reset()
    {
        _upgrades.Clear();
    }
}
