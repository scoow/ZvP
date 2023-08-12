public class InputStateMachine
{
    public InputStateType InputState { get; private set; }

    public InputStateMachine()
    {
        InputState = InputStateType.defaultState;
    }

    public void SetNewState(InputStateType newInputStateType)
    {
        InputState = newInputStateType;
    }
}