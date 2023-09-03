using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Health Power-up")]
public class HealthBuffSO : PowerupBaseSO
{
    [SerializeField] float healthIncreaseAmount;

    public override float Modify(float initialValue)
    {
        var newHealth = initialValue + healthIncreaseAmount;
        healthIncreaseAmount = 0f; // Ensures health buff is one-time event
        return newHealth;
    }
}
