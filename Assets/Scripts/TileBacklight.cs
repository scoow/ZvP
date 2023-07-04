using UnityEngine;
using UnityEngine.EventSystems;

namespace ZvP.UI
{
    [RequireComponent(typeof(Collider2D))]
    public class TileBacklight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private SpriteRenderer _spriteRenderer;
        [SerializeField]
        private Color _backlightColor;
        private Color _tempColor;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            _tempColor = _spriteRenderer.color;
            _spriteRenderer.color = _backlightColor;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _spriteRenderer.color = _tempColor;
        }
    }
}