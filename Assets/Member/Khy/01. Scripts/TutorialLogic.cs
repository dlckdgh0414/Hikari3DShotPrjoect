using DG.Tweening;
using MoreMountains.Feedbacks;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialLogic : MonoBehaviour
{
    public Dialogue[] dialogue;

    [SerializeField] private GameEventChannelSO uiChannel;
    [SerializeField] private MMF_Player inDialogueFeedback;
    [SerializeField] private Sprite spaceMan;

    private int currentDialogue;


    private void Awake()
    {
        DownText();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            SceneManager.LoadScene("ShipStation");
    }
    public void DownText()
    {
        TextEvent textEvent = UIEvents.TextEvent;
        textEvent.Text = dialogue[currentDialogue].helpMeassage;
        textEvent.TextSkipKey = dialogue[currentDialogue].skipKey;
        uiChannel.RaiseEvent(textEvent);
    }
    public void DelayCallDownText()
    {
        DOVirtual.DelayedCall(2f,()=> DownText());
    }
    public void NextDialogue()
        => ++currentDialogue;
    public void StartDialogue()
    {
        StartDialogueEvent dialogueEvent = UIEvents.StartDialogueEvent;
        dialogueEvent.dialogue = dialogue[currentDialogue].dialogue;
        dialogueEvent.characterIllustration = spaceMan;
        dialogueEvent.feedback = inDialogueFeedback;
        uiChannel.RaiseEvent(dialogueEvent);
    }
}
[Serializable]
public struct Dialogue
{
    public string[] dialogue;
    public string helpMeassage;
    public KeyCode skipKey;
}
