using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button _simplePlantButton;
    [SerializeField] private Button _meleePlantButton;
    public InputStateMachine InputStateMachine { get; private set; }
    public PlantType SelectedPlantType { get; private set; }

    private void Awake()
    {
        InputStateMachine = new InputStateMachine();
    }

    /// <summary>
    /// Подписываем кнопки на события
    /// </summary>
    private void OnEnable()
    {
        _simplePlantButton.onClick.AddListener(() => OnPlantButtonClick(PlantType.simplePlant));
        _meleePlantButton.onClick.AddListener(() => OnPlantButtonClick(PlantType.meleePlant));
    }

    private void OnPlantButtonClick(PlantType plantType)
    {
        if (InputStateMachine.InputState != InputStateType.defaultState) return;
        //меняем состояние на "установка растения"
        InputStateMachine.SetNewState(InputStateType.plantButtonPressed);
        SelectedPlantType = plantType;
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