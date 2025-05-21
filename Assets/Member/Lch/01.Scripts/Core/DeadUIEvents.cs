using UnityEngine;

public static class DeadUIEvents
{
    public static DeadEvent DeadEvent = new DeadEvent();
}

public class DeadEvent : GameEvent
{
    public bool isDead;

    public DeadEvent Initailzer(bool isDead)
    {
        this.isDead = isDead;
        return this;
    }
}