using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Button _simplePlantButton;
    [SerializeField]
    private Button _meleePlantButton;
    private InputStateMachine _inputStateMachine;
    public InputStateMachine InputStateMachine => _inputStateMachine;
    private PlantType _selectedPlantType;
    public PlantType SelectedPlantType => _selectedPlantType;
    private void Awake()
    {
        _inputStateMachine = new InputStateMachine();
    }
    /// <summary>
    /// Подписываем кнопки на события
    /// </summary>
    private void OnEnable()
    {
        _simplePlantButton.onClick.AddListener(delegate { OnPlantButtonClick(PlantType.simplePlant); });
        _meleePlantButton.onClick.AddListener(delegate { OnPlantButtonClick(PlantType.meleePlant); });
    }
    private void OnPlantButtonClick(PlantType plantType)
    {
        if (_inputStateMachine.InputState != InputStateType.defaultState) return;
        //меняем состояние на "установка растения"
        _inputStateMachine.SetNewState(InputStateType.plantButtonPressed);
        _selectedPlantType = plantType;
    }
    /// <summary>
    /// Отписываем кнопки от событий
    /// </summary>
    private void OnDisable()
    {
        _simplePlantButton.onClick.RemoveAllListeners();
        _meleePlantButton.onClick.RemoveAllListeners();
    }

}