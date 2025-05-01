namespace Member.Kmin._01_Script.Core.EventChannel
{
    public class SkillEquipEventChannel
    {
        public static SkillSelectEvent SkillSelectEvent = new SkillSelectEvent();
        public static SkillEquipEvent SkillEquipEvent = new SkillEquipEvent();
    }

    public class SkillSelectEvent : GameEvent
    {
        public SkillSO selectedSkill;
    }
    
    public class SkillEquipEvent : GameEvent {}
}