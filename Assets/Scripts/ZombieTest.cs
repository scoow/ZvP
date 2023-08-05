using UnityEngine;

public class ZombieTest : MonoBehaviour
{
    private Collider2D _collider;
    private float _timer;
    [SerializeField] private float _cooldown;
    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
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
}