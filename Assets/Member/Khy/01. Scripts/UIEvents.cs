using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.UI;

public static class UIEvents
{
    public static StartDialogueEvent StartDialogueEvent = new();
    public static TextEvent TextEvent = new();
}

public class StartDialogueEvent : GameEvent
{
    public string[] dialogue;
    public MMF_Player feedback;
    public Sprite characterIllustration;
}

public class TextEvent : GameEvent
{
    public string Text;
    public KeyCode TextSkipKey;
}