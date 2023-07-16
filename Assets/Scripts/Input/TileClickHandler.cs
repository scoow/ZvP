using UnityEngine;
using UnityEngine.EventSystems;

public class TileClickHandler : MonoBehaviour, IPointerClickHandler
{
    private UIManager _uiManager;

    private void Start()
    {
        _uiManager = FindObjectOfType<UIManager>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (_uiManager._inputStateMachine.InputState != InputStateType.plantButtonPressed) return;

        _uiManager._inputStateMachine.SetNewState(InputStateType.defaultState);
        Debug.Log($"Установлено растение {_uiManager._selectedPlantType}");
    }
}