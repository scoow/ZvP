public enum GameEventType : byte
{
    zombieSpawned,
    zombieDied,
    plantSpawned,
    plantDied,
    plantShooted
}

public enum InputStateType : byte
{
    defaultState,
    plantButtonPressed
}

public enum PlantType : byte
{
    simplePlant,
    meleePlant
}