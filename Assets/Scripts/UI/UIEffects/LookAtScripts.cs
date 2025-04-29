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

        // ��ǥ ȸ�� ���ʹϾ� ����
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // ��ǥ ȸ���� ���Ϸ� �� ����
        Vector3 euler = targetRotation.eulerAngles;

        // Y������ -180 ~ 180 �������� �����ؾ� ��Ȯ
        float y = euler.y > 180f ? euler.y - 360f : euler.y;
        float x = euler.x > 180f ? euler.y - 360f : euler.y;
        // Y�� ȸ�� ���� ���� (-25 ~ 25��)
        y = Mathf.Clamp(y, -25f, 25f);
        x = Mathf.Clamp(x, -25f, 25f);
        // ������ Y�� �ٽ� 0~360 ������ ��ȯ
        euler.y = y < 0f ? y + 360f : y;
        euler.x = x < 0f ? x + 360f : x;
        // ���� ȸ�� ���ʹϾ� ����
        targetRotation = Quaternion.Euler(euler);

        // �ε巴�� ȸ��
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime * 100f
        );
    }
}