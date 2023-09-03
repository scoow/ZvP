using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Movement speed Power-up")]
public class MovementSpeedBuffSO : PowerupBaseSO
{
    [SerializeField] float speedIncreaseAmount;

    public override float Modify(float initialValue)
    {
        return initialValue * speedIncreaseAmount;
    }
}
