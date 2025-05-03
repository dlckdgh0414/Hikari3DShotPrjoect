using DG.Tweening;
using Member.Ysc._01_Code.Agent;
using Unity.Behavior;
using UnityEngine;

public abstract class Enemy :Entity
{
    protected BehaviorGraphAgent btAgent;
    protected StateEventChange _stateChannel;
    [field: SerializeField] public EntityFinderSO PlayerFinder { get; protected set; }

    public bool IsDeadEnd {get; protected set; } = false;

    protected override void AfterInitialize()
    {
        base.AfterInitialize();
        btAgent = GetComponent<BehaviorGraphAgent>();
        Debug.Assert(btAgent != null, $"{gameObject.name} does not have an BehaviorGraphAgent");
    }

    public void LookTarget(Transform target)
    {
        transform.LookAt(target);
    }

    public BlackboardVariable<T> GetBlackboardVariable<T>(string key)
    {
        if (btAgent.GetVariable(key, out BlackboardVariable<T> result))
        {
            return result;
        }

        return default;
    }

    protected virtual void Start()
    {

    }

    [ContextMenu("Enemy Dead")]
    public void EnemyDead()
    {

        // movement.isMove = false;
        transform.DORotate(new Vector3(-35f, 0f, 0f), 0.5f, RotateMode.Fast)
            .OnUpdate(() =>
            {
                Vector3 pos = new Vector3(transform.position.x, transform.forward.y, transform.forward.z * -0.4f);
                transform.DOMove(pos, 10f);
            }).OnComplete(() => IsDeadEnd = true);
    }



}
