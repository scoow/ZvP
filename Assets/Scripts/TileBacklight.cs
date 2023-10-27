using UnityEngine;
using UnityEngine.EventSystems;

namespace ZvP.UI
{
    [RequireComponent(typeof(Collider2D))]
    public class TileBacklight : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private Collider2D _collider;

        [SerializeField] private Color _backlightColor;
        private Color _originalColor;
        private bool _colorChanged = false;

        private LayerMask _tileLayer = new LayerMask();

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _collider = GetComponent<Collider2D>();
        }

        private void Start()
        {
            _tileLayer = LayerMask.GetMask("Tiles");
            _originalColor = _spriteRenderer.color;
        }
    
        private void Update()
        {
            var mousePos = Input.mousePosition;
            mousePos.z = 10;

            var from = Camera.main.ScreenToWorldPoint(mousePos);
            var tileCollider = Physics2D.OverlapPoint(from, _tileLayer);
            if (tileCollider == _collider)
            {
                if (!_colorChanged)
                {
                    _spriteRenderer.color = _backlightColor;
                    _colorChanged = true;
                }
            } 
            else
            {
                if (_colorChanged)
                {
                    _spriteRenderer.color = _originalColor;
                    _colorChanged = false;
                }
            }
        }    
    }
}