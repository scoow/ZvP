using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerClicksProcessor : MonoBehaviour
{
    private Collider2D[] _mouseClickHits = new Collider2D[10];
    private LayerMask _zombieLayer = new LayerMask();

    [SerializeField] private UpgradeItemSO _testUpgrade;

    void Start()
    {
        _zombieLayer = LayerMask.GetMask("Zombie");
    }

    void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        var mousePos = Input.mousePosition;
        mousePos.z = 10;

        var from = Camera.main.ScreenToWorldPoint(mousePos);
        _mouseClickHits = Physics2D.OverlapPointAll(from, _zombieLayer);
        if (_mouseClickHits.Length > 0)
        {
            TryApplyUpgrade();
        }
    }

    private void TryApplyUpgrade()
    {
        var zombieCollider = GetNearestObject();
        var zombieUpgradeReceiver = zombieCollider.GetComponent<UpgradeReceiver>();
        if (zombieUpgradeReceiver == null) return;

        var clone = _testUpgrade.Clone();
        if (zombieUpgradeReceiver.Apply(clone))
        {
            Debug.Log("Upgrade set " + clone.name);
        }
        else
        {
            Debug.Log("Upgrade NOT set");
        }
    }

    private Collider2D GetNearestObject()
    {
        Collider2D nearest = null;
        foreach (var zombie in _mouseClickHits)
        {
            if (nearest == null)
            {
                nearest = zombie;
                continue;
            }
            if (nearest.transform.position.y > zombie.transform.position.y)
            {
                nearest = zombie;
            }
        }
        return nearest;
    }
}
