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
    /// <summary>
    /// ��������� ������� �� ����
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (_uiManager.InputStateMachine.InputState != InputStateType.plantButtonPressed) return;

        _uiManager.InputStateMachine.SetNewState(InputStateType.defaultState);
        Debug.Log($"����������� �������� {_uiManager.SelectedPlantType}");

        switch (_uiManager.SelectedPlantType)
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