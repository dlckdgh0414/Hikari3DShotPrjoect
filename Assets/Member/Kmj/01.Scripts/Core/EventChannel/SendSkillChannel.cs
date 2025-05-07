namespace Member.Kmj._01.Scripts.Core.EventChannel

{
    public class SendSkillChannel : GameEvent
    {
        public static SendStaticSkill staticSkillEquipEvent = new SendStaticSkill();
        public static SendSkill SkillEquipEvent = new SendSkill();
        public static SkillSendEvent SkillSendEvent = new SkillSendEvent();
    }

    public class SendStaticSkill : GameEvent
    {
        public string staticSkill;
    }

    public class SendSkill : GameEvent
    {
        public string selectedSkill;
    }
    
    public class SkillSendEvent : GameEvent {}
}
