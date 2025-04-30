using UnityEngine;

public class RollEventChannel
{
    public static RollStartEvent rollStartEvent = new RollStartEvent();
    public static RollEndEvent RollEndEvent = new RollEndEvent();
}

public class RollStartEvent : GameEvent { }

public class RollEndEvent : GameEvent
{
    public SkillSO rolledSkill;
}

