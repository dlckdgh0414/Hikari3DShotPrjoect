using DG.Tweening;
using Member.Kmin._01_Script.SkillTree;
using UnityEngine;

public class Player : Entity
{

    [field: SerializeField]
    public InputReader InputReader { get; private set; }



    [SerializeField] private StateListSO playerFSM;

    [field: SerializeField]
    public GameObject model { get; private set; }

    private StateMachine _stateMachine;
    [field: SerializeField] public float zPos { get; set; }

    #region ¿¹Çà
    public float edgeThreshold = 100f;
    public CanvasGroup inGameUI;
    #endregion

    protected override void Awake()
    {
        base.Awake();
        _stateMachine = new StateMachine(this, playerFSM);
    }

    private void Start()
    {
        SaveNodeStat.Instance.LoadStat();
        _stateMachine.ChangeState("IDLE");
    }
    
    private void Update()
    {
        _stateMachine.UpdateStateMachine();
        UIFade();
    }

    public void SetGameUI(bool isActive)
    {
        int fade = isActive ? 1 : 0;
        DOTween.To(() => inGameUI.alpha, x => inGameUI.alpha = x, fade, 0.2f);
    }

    public void SetGameUI(float fade,Ease ease)
    {
        DOTween.To(() => inGameUI.alpha, x => inGameUI.alpha = x, fade, 0.2f).SetEase(ease);
    }

    private void UIFade()
    {
        if (!IsGameStart) return;

        Vector3 screenPos = Camera.main.WorldToScreenPoint(model.transform.position);
        Vector2 screenSize = new(Screen.width, Screen.height);

        Vector2 edgeDirection = Vector2.zero;

        if (screenPos.x <= edgeThreshold)
            edgeDirection.x = -1; // Left
        else if (screenPos.x >= screenSize.x - edgeThreshold)
            edgeDirection.x = 1;  // Right

        if (screenPos.y <= edgeThreshold)
            edgeDirection.y = -1; // Bottom
        //else if (screenPos.y >= screenSize.y - edgeThreshold)
        //    edgeDirection.y = 1;  // Top

        if (edgeDirection != Vector2.zero)
            SetGameUI(0.1f,Ease.OutQuart);
        else
            SetGameUI(1, Ease.OutSine);
    }

    private void FixedUpdate()
    {
        _stateMachine.FixedUpdateStateMachine();
    }

    protected override void AfterInitialize()
    {
        base.AfterInitialize();
    }

    public void ModelChange(GameObject newSkin)
        => model = newSkin;

    protected override void HandleDead()
    {
        if (IsDead) return;
        gameObject.layer = DeadBodyLayer;
        IsDead = true;
        _stateMachine.ChangeState("DEAD");
    }

    public void SetZpos(float changeZPos,float second)
    => DOTween.To(()=> zPos,x=> zPos=x, changeZPos, second);

    protected override void HandleHit()
    {

    }


    public void ChangeState(string newState)
        => _stateMachine.ChangeState(newState);
}
