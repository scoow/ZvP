using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ZombieHealth : Health
{
    private UpgradeReceiver _upgradeReceiver;

    protected override void Awake()
    {
        base.Awake();
        _upgradeReceiver = GetComponent<UpgradeReceiver>();
    }

    private void OnEnable()
    {
        _upgradeReceiver.upgradeAppliedEvent.AddListener(OnUpgradeApplied);
    }

    private void OnDisable()
    {
        _upgradeReceiver.upgradeAppliedEvent.RemoveListener(OnUpgradeApplied);
    }

    public override void ProcessHit(float damage)
    {
        var damageLeft = _upgradeReceiver.ProcessValue(UpgradeableAttribute.Armor, damage);
        base.ProcessHit(damageLeft);
    }

    private void OnUpgradeApplied(IEnumerable<UpgradeableAttribute> modifiedAttributes)
    {
        if (modifiedAttributes.Contains(UpgradeableAttribute.Health))
        {
            _currentHealth = _upgradeReceiver.ProcessValue(UpgradeableAttribute.Health, _currentHealth);
        }
    }
}
