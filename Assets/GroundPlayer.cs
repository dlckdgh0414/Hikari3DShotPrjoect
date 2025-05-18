using UnityEngine;
using UnityEngine.AI;

public class GroundPlayer : MonoBehaviour
{
    [SerializeField]
    private InputReader playerInput;
    [SerializeField]
    private NavMeshAgent _movement;
    private PlayAnims anims;

    [SerializeField] private float stopThreshold = 0.6f;
    private Vector3 _destination;
    public bool IsArrived => Vector3.Distance(transform.position, _destination) < stopThreshold;

    [SerializeField]
    private Unit currentUnit;

    private void Awake()
    {
        anims = GetComponent<PlayAnims>();
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
            ProcessClickSelection();

        if(_movement.remainingDistance <= _movement.stoppingDistance)
        {
            anims.OnButtonClickWithAnimName("Happy Idle");

            if (currentUnit != null)
                currentUnit.IsArrived();
        }
    }

    private void ProcessClickSelection()
    {
        if (currentUnit != null)
            currentUnit.SetSelected(false);

        Vector3 worldPosition = playerInput.GetWorldPosition(out RaycastHit hitInfo);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.TryGetComponent(out Unit unit))
            {                
                currentUnit = unit;
                unit.SetSelected(true);
            }

            Quaternion look = Quaternion.LookRotation(worldPosition);
            transform.rotation = Quaternion.Lerp(transform.rotation , look,0.2f);

            _movement.isStopped = false;
            _movement.SetDestination(worldPosition);
            _destination = worldPosition;
            anims.OnButtonClickWithAnimName("Catwalk Walking");
        }
    }
}
