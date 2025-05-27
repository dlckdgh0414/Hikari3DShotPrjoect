using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameEventChannelSO uiChannel;

    [SerializeField]
    private RectTransform upImage;
    [SerializeField]
    private RectTransform downImage;

    [Header("위아래 까만판넬 움직이는 시간")]
    [SerializeField]
    private float moveUiSpeed;

    [Header("페이드")]
    [SerializeField]
    private float fadeTime=0.2f;

    [SerializeField]
    private InputReader InputReader;

    [SerializeField]
    private Animator[] _animtor;


    [Header("캐릭터 대사")]
    [SerializeField]
    private TextMeshProUGUI _text;
    [Header("캐릭터 사진")]
    [SerializeField]
    private Image characterImage;

    [Space(10)]

    [Header("밑에 뜨는 문구")]
    [SerializeField]
    private TextMeshProUGUI _downText;
    [SerializeField]
    private float duration=1f;
    private KeyCode skipCode;
    private bool isShowText;
    public UnityEvent OnClickEventKey;
    public UnityEvent OnEndDialogueEvent;

    private string[] currentDialogue;
    private bool isDialogue;
    private int textNum = 0;

    private void Awake()
    {
        uiChannel.AddListener<StartDialogueEvent>(HandleDialogueStart);
        uiChannel.AddListener<TextEvent>(HandleTextEvent);
    }

    private void HandleTextEvent(TextEvent obj)
    {
        _downText.DOFade(1f, 0.2f);
        skipCode = obj.TextSkipKey;
        _downText.DOText(obj.Text,duration);
        isShowText = true;
    }

    private void Update()
    {
        if(Input.GetKeyDown(skipCode)&&isShowText)
        {
            isShowText = false;
            _downText.DOFade(0f,0.2f).OnComplete(()=>_downText.text = "");
            OnClickEventKey?.Invoke();
        }

        if(Input.GetMouseButtonDown(0) && isDialogue && !isShowText)
        {
            if (textNum+1 == currentDialogue.Length)
            {
                EndTalk();
                return;
            }
            else
                NextTalk();
        }
    }

    private void NextTalk()
    {
        ++textNum;
        _text.text = currentDialogue[textNum];
    }

    private void EndTalk()
    {
        Time.timeScale = 1f;
        isDialogue = false;
        textNum = 0;
        for (int i = 0; i < _animtor.Length; i++)
            _animtor[i].Play("OutAnimation");
        upImage.DOScaleY(0f, moveUiSpeed);
        downImage.DOScaleY(0f, moveUiSpeed);
        OnEndDialogueEvent?.Invoke();
    }

    private void SettingScene()
    {
        upImage.DOScaleY(1f,moveUiSpeed);
        downImage.DOScaleY(1f,moveUiSpeed);
    }

    private void HandleDialogueStart(StartDialogueEvent obj)
    {
        SettingScene();
        currentDialogue = obj.dialogue;
        StartTalk();
    }

    private void StartTalk()
    {
        for (int i = 0; i < _animtor.Length; i++)
            _animtor[i].Play("InAnimation");
        _text.text = currentDialogue[0];
        isDialogue = true;
    }

    private void OnDestroy()
    {
        uiChannel.RemoveListener<StartDialogueEvent>(HandleDialogueStart);
        uiChannel.RemoveListener<TextEvent>(HandleTextEvent);
    }
}
