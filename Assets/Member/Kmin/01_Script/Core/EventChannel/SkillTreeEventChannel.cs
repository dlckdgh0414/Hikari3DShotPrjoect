using UnityEngine;

public class SkillTreeEventChannel : MonoBehaviour
{
    public static SkillTreeSelectEvent SkillTreeSelectEvent = new SkillTreeSelectEvent();
    public static SkillTreePurchaseEvent SkillTreePurchaseEvent = new SkillTreePurchaseEvent();
    public static SkillTreeActiveEvent SkillTreeActiveEvent = new SkillTreeActiveEvent();
}

public class SkillTreeSelectEvent : GameEvent
{
    public SkillTreeNode node;
}

public class SkillTreePurchaseEvent : GameEvent
{
    public SkillTreeNode node;
}

public class SkillTreeActiveEvent : GameEvent
{
    public bool isActive;
}

