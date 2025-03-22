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

        spawnPos += new Vector3(Random.Range(-10f, 10f), Random.Range(-5f, 5f), 0);

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

    public Vector3 GetPatolMove()
    {

        Camera cam = Camera.main;

        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;

        float randomX = cam.transform.position.x + Random.Range(-width / 2f, width / 2f) * 0.9f;
        float randomY = cam.transform.position.y + Random.Range(-height / 2f, height / 2f) * 0.9f;
        float randomZ = cam.transform.position.z;

        Vector3 randomPos = new Vector3(randomX, randomY, randomZ);
        return randomPos;
    }

    public void PatolMove(Vector3 Dir)
    {

        if (!isMove)
            return;

        Vector3 movDir = new Vector3(Dir.x - transform.position.x, Dir.y - transform.position.y , 0);

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
