using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Should be added as component to upgradeable GameObject (zombie).
/// Manages upgrades to each body part. No more than one upgrade could be applied to one body part.
/// </summary>
public class UpgradeReceiver : MonoBehaviour
{
    /// <summary>
    /// Informs subscribers of what attribues has been changed by applied upgrade 
    /// </summary>
    public UnityEvent<IEnumerable<UpgradeableAttribute>> upgradeAppliedEvent;

    private Dictionary<BodyPart, UpgradeItemSO> _upgrades;

    private void Awake()
    {
        if (upgradeAppliedEvent == null)
            upgradeAppliedEvent = new UnityEvent<IEnumerable<UpgradeableAttribute>>();

        if (_upgrades == null)
            _upgrades = new Dictionary<BodyPart, UpgradeItemSO>() { };
    }

    /// <summary>
    /// Registers UpgradeItemSO as upgrade to a particular body part
    /// </summary>
    /// <param name="upgrade"></param>
    /// <returns>false, if upgrade couldn't be applied</returns>
    public bool Apply(UpgradeItemSO upgrade)
    {
        if (_upgrades.ContainsKey(upgrade.AppliesTo))
        {
            return false;
        }

        if (upgrade.GetPowerupsFor(UpgradeableAttribute.None).Any()) 
        {
            throw new System.Exception("Could't apply upgrade with power-ups that has None as their UpgradeableAttribute");
        }

        _upgrades[upgrade.AppliesTo] = upgrade;
        upgrade.SetOnDestroyAction(() =>
        {
            if (_upgrades.Remove(upgrade.AppliesTo))
            {
                // Subscribed components should recalculate UpgradeableAttributes they are interested in
                upgradeAppliedEvent?.Invoke(upgrade.GetModifiedAttributes());
            }
        });

        upgradeAppliedEvent?.Invoke(upgrade.GetModifiedAttributes());

        return true;
    }

    /// <summary>
    /// Modifies <paramref name="initialValue"/> with cumulative effect of each powerup that can be applied to <paramref name="attribute"/>
    /// </summary>
    /// <param name="attribute">powerups are filtered by this attribute value</param>
    /// <param name="initialValue">value before applying powerup modifiers</param>
    /// <returns>value after changes</returns>
    public float ProcessValue(UpgradeableAttribute attribute, float initialValue)
    {
        float result = initialValue;
        foreach (var upgrade in _upgrades.Values.ToArray())
        {
            var powerups = upgrade.GetPowerupsFor(attribute);
            result = ApplyModifiersToValue(powerups, result);
        }
        return result;
    }

    private float ApplyModifiersToValue(IEnumerable<PowerupBaseSO> powerups, float initialValue)
    {
        float result = initialValue;
        foreach (var powerup in powerups)
        {
            result = powerup.Modify(initialValue);
        }
        return result;
    }

    /// <summary>
    /// Should be called before releasing to object pool or after take from pool
    /// </summary>
    public void Reset()
    {
        _upgrades.Clear();
    }
}
