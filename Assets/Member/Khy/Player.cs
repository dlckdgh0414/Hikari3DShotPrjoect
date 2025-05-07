using DG.Tweening;
using UnityEngine;

public class Player : Entity
{
    [field : SerializeField]
    public InputReader InputReader { get; private set; }

    [SerializeField] private StateListSO playerFSM;

    [field: SerializeField] public GameObject model { get; private set; }

    private StateMachine _stateMachine;
    [field: SerializeField] public float zPos { get; set; }

    


    protected override void Awake()
    {
        base.Awake();
        _stateMachine = new StateMachine(this, playerFSM);
    }

    private void Start()
    {
        _stateMachine.ChangeState("IDLE");
    }

    private void Update()
    {
        _stateMachine.UpdateStateMachine();
    }

    private void FixedUpdate()
    {
        _stateMachine.FixedUpdateStateMachine();
    }

    protected override void AfterInitialize()
    {
        base.AfterInitialize();
    }

    protected override void HandleDead()
    {
        if (IsDead) return;
        gameObject.layer = DeadBodyLayer;
        IsDead = true;
        _stateMachine.ChangeState("DEAD");
    }

    public void SetZpos(float changeZPos)
    => DOTween.To(()=> zPos,x=> zPos=x, changeZPos, 0.2f);

    protected override void HandleHit()
    {
        throw new System.NotImplementedException();
    }


    public void ChangeState(string newState)
        => _stateMachine.ChangeState(newState);
}
