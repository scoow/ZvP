using UnityEngine;
using UnityEngine.EventSystems;

public class ZombieTest : MonoBehaviour, IPointerClickHandler
{
    private Collider2D _collider;
    private UpgradeReceiver _upgradeReceiver;

    private float _timer;
    [SerializeField] private float _cooldown;
    [SerializeField] private UpgradeItemSO _testUpgrade;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _upgradeReceiver = GetComponent<UpgradeReceiver>();
        _timer = _cooldown;
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer < 0)
        {
            _timer = _cooldown;

            RaycastHit2D[] results = new RaycastHit2D[13];
            _collider.Cast(Vector2.left, results, 20);

            foreach (RaycastHit2D r in results)
            {
                if (r.collider != null && r.collider.TryGetComponent<PlantHealth>(out _))
                    r.collider.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.red;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_upgradeReceiver != null)
        {
            var clone = _testUpgrade.Clone();
            if (_upgradeReceiver.Apply(clone))
            {
                Debug.Log("Upgrade set "+ clone.name);
            } else
            {
                Debug.Log("Upgrade NOT set");
            }
        }
    }
}