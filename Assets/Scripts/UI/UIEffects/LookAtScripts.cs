using UnityEngine;

public class LookAtScripts : MonoBehaviour
{
    public float rotationSpeed = 5f;

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Vector3.Distance(transform.position, Camera.main.transform.position);

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3 direction = worldPos - transform.position;

        if (direction.sqrMagnitude < 0.001f) return;

        // 목표 회전 쿼터니언 생성
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // 목표 회전의 오일러 각 추출
        Vector3 euler = targetRotation.eulerAngles;

        // Y각도는 -180 ~ 180 기준으로 제한해야 정확
        float y = euler.y > 180f ? euler.y - 360f : euler.y;
        float x = euler.x > 180f ? euler.y - 360f : euler.y;
        // Y축 회전 각도 제한 (-25 ~ 25도)
        y = Mathf.Clamp(y, -25f, 25f);
        x = Mathf.Clamp(x, -25f, 25f);
        // 제한한 Y를 다시 0~360 범위로 변환
        euler.y = y < 0f ? y + 360f : y;
        euler.x = x < 0f ? x + 360f : x;
        // 최종 회전 쿼터니언 생성
        targetRotation = Quaternion.Euler(euler);

        // 부드럽게 회전
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime * 100f
        );
    }
}