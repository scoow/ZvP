using UnityEngine;

public class UnitsLayerSorter : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Transform _parent;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _parent = transform.parent;
    }

    public void ResetLayer()
    {
        _spriteRenderer.sortingOrder = 1;
    }
    public void AddLayer(int LayerIncrement)
    {
        _spriteRenderer.sortingOrder += LayerIncrement;
    }
}