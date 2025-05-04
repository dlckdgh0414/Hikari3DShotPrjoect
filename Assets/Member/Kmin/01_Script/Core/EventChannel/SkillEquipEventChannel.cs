namespace Member.Kmin._01_Script.Core.EventChannel
{
    public class SkillEquipEventChannel
    {
        public static SkillSelectEvent SkillSelectEvent = new SkillSelectEvent();
        public static SkillEquipEvent SkillEquipEvent = new SkillEquipEvent();
        public static StaticSelectEvent staticSkillEquipEvent = new StaticSelectEvent();
    }

    public class SkillSelectEvent : GameEvent
    {
        public SkillSO selectedSkill;
    }

    public class StaticSelectEvent : GameEvent
    {
        public SkillSO staticSkill;
    }
    
    public class SkillEquipEvent : GameEvent {}
}