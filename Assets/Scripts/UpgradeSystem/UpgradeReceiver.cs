using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

    // Animation
    private Animator animator;
    private AnimatorOverrideController animatorOverrideController;
    private AnimationClipOverrides defaultAnimationClips;
    private bool animationsOverrided = false;

    private void Awake()
    {
        if (upgradeAppliedEvent == null)
            upgradeAppliedEvent = new UnityEvent<IEnumerable<UpgradeableAttribute>>();

        if (_upgrades == null)
            _upgrades = new Dictionary<BodyPart, UpgradeItemSO>() { };
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = animatorOverrideController;

        defaultAnimationClips = new AnimationClipOverrides(animatorOverrideController.overridesCount);
        animatorOverrideController.GetOverrides(defaultAnimationClips);
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
            // Body part already upgraded
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
                ResetAnimationClips();
            }
        });

        upgradeAppliedEvent?.Invoke(upgrade.GetModifiedAttributes());

        setAnimationClips(upgrade.animations, upgrade.AppliesTo);

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

    private void setAnimationClips(UpgradeItemSO.KindToAnimationEntry[] animations, BodyPart appliesTo)
    {
        foreach (var item in animations)
        {
            defaultAnimationClips[$"{appliesTo}_{item.Kind}"] = item.AnimationClip;
            animationsOverrided = true;
        }
        animatorOverrideController.ApplyOverrides(defaultAnimationClips);
    }

    private void ResetAnimationClips()
    {
        if (!animationsOverrided) return;

        defaultAnimationClips.ResetClips();
        animatorOverrideController.ApplyOverrides(defaultAnimationClips);
        animationsOverrided = false;
    }

    /// <summary>
    /// Should be called before releasing to object pool or after take from pool
    /// </summary>
    public void Reset()
    {
        _upgrades.Clear();
        ResetAnimationClips();
    }
}

public class AnimationClipOverrides : List<KeyValuePair<AnimationClip, AnimationClip>>
{
    public AnimationClipOverrides(int capacity) : base(capacity) { }

    public AnimationClip this[string name]
    {
        get { return this.Find(x => x.Key.name.Equals(name)).Value; }
        set
        {
            int index = this.FindIndex(x => x.Key.name.Equals(name));
            if (index != -1)
                this[index] = new KeyValuePair<AnimationClip, AnimationClip>(this[index].Key, value);
        }
    }

    public void ResetClips()
    {
        for (int i = 0; i < this.Count; i++)
        {
            this[i] = new KeyValuePair<AnimationClip, AnimationClip>(this[i].Key, null);
        }
    }
}
