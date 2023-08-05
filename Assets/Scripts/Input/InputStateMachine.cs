public class InputStateMachine
{
    private InputStateType _inputState;
    public InputStateType InputState => _inputState;

    public InputStateMachine()
    {
        _inputState = InputStateType.defaultState;
    }
    public void SetNewState(InputStateType newInputStateType)
    {
        _inputState = newInputStateType;
    }
}