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
        public Skill selectedSkill;
    }

    public class StaticSelectEvent : GameEvent
    {
        public Skill staticSkill;
    }
    
    public class SkillEquipEvent : GameEvent {}
}