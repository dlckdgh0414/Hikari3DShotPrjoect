using TMPro;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.GraphicsBuffer;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    [SerializeField] private float minDistance = 3f;
    [SerializeField] private Camera cam;

    public Vector3 GetMovePos()
    {
        cam = Camera.main;
        float x = Random.Range(0.2f, 0.8f);
        float y = Random.Range(0.2f, 0.8f);
        float z = cam.nearClipPlane + 10f;

        Vector3 viewPos = new Vector3(x, y, z);
        return cam.ViewportToWorldPoint(viewPos);
    }
    public void Move(Transform player,Vector3 targetPosition)
    {

        Vector3 directionToTarget = (targetPosition - transform.position);
        float distanceToTarget = directionToTarget.magnitude;
        if (distanceToTarget < 0.1f)
        {
            Debug.Log("목표에 도착! 이동 멈춤");
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        Debug.Log($"현재 위치: {transform.position}, 목표 위치: {targetPosition}");


        if (directionToTarget.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
    }

    public void PatolMove(Vector2 min, Vector2 max)
    {
        float x = Random.Range(min.x, max.x);
        float y = Random.Range(min.y, max.y);

        //rb.linearVelocity = new Vector2 (x, y) * speed;
    }
}
