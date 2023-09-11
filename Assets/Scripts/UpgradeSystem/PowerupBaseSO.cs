using UnityEngine;

/// <summary>
/// Represents changes to value that is related to one UpgradeableAttribute.
/// 
/// i.g. armor reduces damage - armor here is UpgradeItem that has powerup that reduces value of received damage.
/// </summary>
public abstract class PowerupBaseSO: ScriptableObject
{
    // property should be used to distinguish powerups that affects different attributes
    public UpgradeableAttribute Modifies = UpgradeableAttribute.None;
    [SerializeField]
    public Sprite PowerupSprite = null;
    /// <summary>
    /// Changes <paramref name="initialValue"/> to reflect effect of powerup
    /// </summary>
    /// <param name="initialValue">value that should be changed</param>
    /// <returns>value after changes</returns>
    public abstract float Modify(float initialValue);
}