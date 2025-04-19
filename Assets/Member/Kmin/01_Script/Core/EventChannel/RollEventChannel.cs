using UnityEngine;

public class RollEventChannel
{
    public static RollEvent RollEvent = new RollEvent();
    public static RollEndEvent RollEndEvent = new RollEndEvent();
}

public class RollEvent : GameEvent
{
    public RollDataSO rolledSkill;
}

public class RollEndEvent : GameEvent
{
    public RollDataSO rolledSkill;
}

