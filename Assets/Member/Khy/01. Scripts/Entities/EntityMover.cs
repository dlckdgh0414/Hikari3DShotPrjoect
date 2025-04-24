using DG.Tweening;
using UnityEngine;

public class EntityMover : MonoBehaviour,IEntityComponent
{
    #region Member field

    [field: SerializeField] public float MoveSpeed { get; private set; }
    [field: SerializeField] public float SpinSpeed { get; private set; }

    private Vector3 _velocity;
    private float _moveSpeedMultiplier;

    private Rigidbody _rbCompo;
    private Vector3 _autoMovement;
    #endregion

    public bool CanManualMove { get; set; } = true;


    public void Initialize(Entity entity)
    {
        _rbCompo = entity.GetComponent<Rigidbody>();
        Debug.Assert(_rbCompo != null,"이거 없음");
        _moveSpeedMultiplier = 1f;
    }

    public void SetMovement(Vector2 moveDir)
    {
        if (moveDir.Equals(Vector2.zero)) return;
        _velocity = new Vector3(moveDir.x, moveDir.y, 0);
    }


    private void FixedUpdate()
    {
        if (_rbCompo == null)
            print("야이쓰방");
        if (CanManualMove)
            _rbCompo.linearVelocity = _velocity * MoveSpeed * _moveSpeedMultiplier;
        else
            _rbCompo.linearVelocity = _autoMovement * SpinSpeed * Time.fixedDeltaTime;
    }

    public void StopImmediately()
    {
        _velocity = Vector3.zero;
        _rbCompo.linearVelocity = Vector3.zero;
    }

    public void SetDecrease(float second)
    {
        DOTween.To(()=> _rbCompo.linearVelocity , x => _rbCompo.linearVelocity = x, Vector3.zero , second).OnComplete(()=>StopImmediately());
    }

    public void SetAutoMovement(Vector3 autoMovement)
         => _autoMovement = autoMovement;
}
