using UnityEngine;
using UnityEngine.EventSystems;

public class TileClickHandler : MonoBehaviour, IPointerClickHandler
{
    private UIManager _uiManager;
    [SerializeField] private PlantHealth _simplePlantPrefab;
    [SerializeField] private PlantHealth _meleePlantPrefab;

    private bool _active = true;

    private void Start()
    {
        _uiManager = FindObjectOfType<UIManager>();
    }
    /// <summary>
    /// Обработка нажатия на тайл
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (_uiManager.InputStateMachine.InputState != InputStateType.plantButtonPressed || !_active) return;

        _uiManager.InputStateMachine.SetNewState(InputStateType.defaultState);
        Debug.Log($"Установлено растение {_uiManager.SelectedPlantType}");

        _active = false;

        switch (_uiManager.SelectedPlantType)
        {
            case PlantType.simplePlant:
                var simplePlant = Instantiate( _simplePlantPrefab, transform.position, Quaternion.identity );
                simplePlant.transform.parent = transform;
                break;
            case PlantType.meleePlant:
                var meleePlant = Instantiate(_meleePlantPrefab, transform.position, Quaternion.identity);
                meleePlant.transform.parent = transform;
                break;
            default:
                break;
        }
    }
}