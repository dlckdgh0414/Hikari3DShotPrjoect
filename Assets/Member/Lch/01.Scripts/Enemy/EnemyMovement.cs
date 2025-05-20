using DG.Tweening;
using UnityEngine;

public class EnemyMovement : MonoBehaviour,IEntityComponent
{
    [SerializeField] private float speed = 300f;
    [SerializeField] private float playerDistance = 20;
    [SerializeField] private int zPos;
    [SerializeField] private Rigidbody rb;
    public bool isMove = true;
    public bool isArrive = false;
    public bool IsCanManulMove = false;
    public void Move(Transform player,Vector3 targetPosition)
    {
        Vector3 targetZPos = new Vector3(Random.Range(-20f,20f), Random.Range(-10f, 10f), zPos);
        rb.DOMove(targetZPos,1.5f).OnComplete(()=>isArrive = true);
    }

    public void StopMover()
    {
        rb.linearVelocity = Vector3.zero;
    }

    public Vector3 GetPatrolMove()
    {

        Camera cam = Camera.main;

        float halfHeight = cam.orthographicSize;
        float halfWidth = halfHeight * cam.aspect;

        float randomX = Random.Range(-halfWidth /3, halfWidth/3);
        float randomY = Random.Range(-halfHeight / 3, halfHeight/ 3);

        Vector3 randomPos = cam.transform.position + new Vector3(randomX, randomY, transform.position.z);

        return randomPos;
    }

    public void PatrolMove(Vector3 Dir)
    {
        Vector3 movDir = new Vector3(Random.Range(-20f,20f), Random.Range(-7.5f, 7.5f), zPos);
        rb.DOMove(movDir,1.5f).OnComplete(()=>isArrive =true);

    }

    public void Initialize(Entity entity)
    {
        
    }
}
