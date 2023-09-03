using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Upgrades/Armor Power-up")]
public class ArmorBuffSO : PowerupBaseSO, IDestroyablePowerup
{
    [Tooltip("Maximum amount of blocked damage")]
    [SerializeField] float _armorValue;

    [Tooltip("Each time armor gets hit its durability lowers by the amount of blocked damage")]
    [SerializeField] float _armorDurability;

    private float _currentDurability;
    public UnityAction _onArmorDestroyed;

    private void Awake()
    {
        ResetToDefaults();
    }

    public override float Modify(float incomingDamage)
    {
        var currentBlockingAbility = Mathf.Min(_currentDurability, _armorValue);
        var blockedDamage = Mathf.Min(currentBlockingAbility, incomingDamage);

        _currentDurability -= blockedDamage;
        if (_currentDurability <= 0 )
        {
            _onArmorDestroyed?.Invoke();
        }

        return incomingDamage - blockedDamage;
    }

    public void OnPowerupDestroyed(UnityAction onArmorDestroyed)
    {
        _onArmorDestroyed = onArmorDestroyed;
    }

    private void ResetToDefaults()
    {
        _currentDurability = _armorDurability;       
    }
}
