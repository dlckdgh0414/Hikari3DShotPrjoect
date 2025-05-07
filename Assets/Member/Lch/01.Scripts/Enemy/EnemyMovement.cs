using UnityEngine;

public class EnemyMovement : MonoBehaviour,IEntityComponent
{
    [SerializeField] private float speed = 8f;
    [SerializeField] private float playerDistance = 20f;
    [SerializeField] private Rigidbody rb;
    public bool isMove = true;
    public bool isArrive = false;

    public Vector2 GetMovePos()
    {
        Camera cam = Camera.main;

        Vector3 spawnPos = cam.transform.position + cam.transform.forward * 20f;

        spawnPos += new Vector3(Random.Range(-20f, 20f), Random.Range(-10f, 10f));

        return spawnPos;
    }
    public void Move(Transform player,Vector3 targetPosition)
    {

        if (!isMove)
            return;

        Vector3 direction = new Vector3((targetPosition.x - transform.position.x),
            (targetPosition.y - transform.position.y),
            (player.transform.position.z + playerDistance - transform.position.z));
        rb.linearVelocity = direction * speed;

        float distance = Vector3.Distance(transform.position, new Vector3(targetPosition.x,targetPosition.y,player.transform.position.z + playerDistance));

        if (distance < 0.2f) 
        {
            rb.linearVelocity = Vector3.zero;
            isMove = false;
            isArrive = true;
        }
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

        float randomX = Random.Range(-halfWidth /2, halfWidth/2);
        float randomY = Random.Range(-halfHeight / 2, halfHeight/ 2);

        Vector3 randomPos = cam.transform.position + new Vector3(randomX, randomY, transform.position.z);

        return randomPos;
    }

    public void PatrolMove(Vector3 Dir)
    {

        if (!isMove)
            return;

        Vector3 movDir = new Vector3(Dir.x - transform.position.x, Dir.y - transform.position.y , 0);
        movDir.Normalize();
        rb.linearVelocity = movDir * speed;

        float distance = Vector2.Distance(transform.position, Dir);

        if (distance < 0.2f)
        {
            rb.linearVelocity = Vector3.zero;
            isMove = false;
            isArrive = true;
        }

    }

    public void Initialize(Entity entity)
    {
        
    }
}
