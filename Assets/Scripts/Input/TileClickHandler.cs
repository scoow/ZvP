using UnityEngine;
using UnityEngine.EventSystems;

public class TileClickHandler : MonoBehaviour, IPointerClickHandler
{
    private UIManager _uiManager;
    [SerializeField] private GameObject _simplePlantPrefab;
    [SerializeField] private GameObject _meleePlantPrefab;

    private void Start()
    {
        _uiManager = FindObjectOfType<UIManager>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (_uiManager._inputStateMachine.InputState != InputStateType.plantButtonPressed) return;

        _uiManager._inputStateMachine.SetNewState(InputStateType.defaultState);
        Debug.Log($"����������� �������� {_uiManager._selectedPlantType}");

        switch (_uiManager._selectedPlantType)
        {
            case PlantType.simplePlant:
                Instantiate( _simplePlantPrefab, transform.position, Quaternion.identity );
                break;
            case PlantType.meleePlant:
                Instantiate(_meleePlantPrefab, transform.position, Quaternion.identity);
                break;
            default:
                break;
        }
    }
}