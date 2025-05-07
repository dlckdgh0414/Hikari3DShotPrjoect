using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class AutoAimCompo : MonoBehaviour,IEntityComponent
{
    private Player _player;

    public GameObject target;

    public Image aim;
    public Image lockAim;
    public Vector2 uiOffset;


    public LayerMask targetLayer; // 검사할 오브젝트 레이어
    public float maxDistance = 100f; // 최대 감지 거리

    public GameObject[] enemies;

    public bool IsAutoAim { get; private set; }

    public void Initialize(Entity entity)
    {
        _player = entity as Player;
        _player.InputReader.OnAutoAimEvent += LockInterface;
    }
    private void OnDestroy()
    {
        _player.InputReader.OnAutoAimEvent -= LockInterface;
    }
    private void Start()
    {
        target = enemies[0];
    }

    private void Update()
    {
        MovePointer();
    }

    void FindClosestObjectToMouse()
    {
        Vector3 mousePos = Input.mousePosition;

        float closestDistance = 200f;

        foreach (GameObject obj in enemies)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(obj.transform.position);
            float distance = Vector2.Distance(mousePos, screenPos);

            if (distance < closestDistance)
            {
                target = obj;
                closestDistance = distance;
            }
        }
    }
    void MovePointer()
    {
        Vector3 destination;

        if (IsAutoAim)
        {
            FindClosestObjectToMouse();
            destination = Camera.main.WorldToScreenPoint(target.transform.position + (Vector3)uiOffset);
        }
        else
            destination = Input.mousePosition;

        aim.transform.DOMove(destination, 0.1f);
    }

    void LockInterface(bool state)
    {
        IsAutoAim = state;
        float size = state ? 1 : 2;
        float fade = state ? 1 : 0;
        lockAim.DOFade(fade, .15f);
        lockAim.transform.DOScale(size, .15f).SetEase(Ease.OutBack);
        lockAim.transform.DORotate(Vector3.forward * 180, .15f, RotateMode.FastBeyond360).From();
        aim.transform.DORotate(Vector3.forward * 90, .15f, RotateMode.LocalAxisAdd);
    }
}
