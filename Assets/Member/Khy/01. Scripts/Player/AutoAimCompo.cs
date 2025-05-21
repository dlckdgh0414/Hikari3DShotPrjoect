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


    public LayerMask targetLayer; // �˻��� ������Ʈ ���̾�
    public float maxDistance = 100f; // �ִ� ���� �Ÿ�

    [field:SerializeField]
    public bool IsAutoAim { get; private set; }

    private bool isUIReciver;

    public void Initialize(Entity entity)
    {
        _player = entity as Player;
    }
    private void OnDestroy()
    {
    }
    private void Start()
    {
        target = null;
        UIAnima(true);
    }

    private void Update()
    {
        MovePointer();

        bool isAuto = EnemyManager.Enemies.Count > 0;
        LockInterface(isAuto);
        if (!isAuto && !isUIReciver)
            UIAnima(true);
        if (isUIReciver && isAuto)
            UIAnima(false);
    }

    void FindClosestObjectToMouse()
    {
        Vector2 mousePos = Input.mousePosition;

        float closestDistance = 200f; //�ʹ� �ִ� �����Ÿ�

        foreach (Enemy obj in EnemyManager.Enemies)
        {
            if (obj.IsDead) continue;
            Vector2 screenPos = Camera.main.WorldToScreenPoint(obj.transform.position);
            float distance = Vector2.Distance(mousePos, screenPos);

            if (distance < closestDistance)
            {
                target = obj.gameObject;
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
            if(target != null)
                destination = Camera.main.WorldToScreenPoint(target.transform.position + (Vector3)uiOffset);
            else
                destination = Input.mousePosition;
        }
        else
            destination = Input.mousePosition;

        aim.transform.DOMove(destination, 0.1f);
    }

    void LockInterface(bool state)
    => IsAutoAim = state;
    

    private void UIAnima(bool isbool)
    {
        float size = isUIReciver ? 1 : 2;
        float fade = isUIReciver ? 1 : 0;
        lockAim.DOFade(fade, .15f);
        lockAim.transform.DOScale(size, .15f).SetEase(Ease.OutBack);
        lockAim.transform.DORotate(Vector3.forward * 180, .15f, RotateMode.FastBeyond360).From();
        aim.transform.DORotate(Vector3.forward * 90, .15f, RotateMode.LocalAxisAdd);
        isUIReciver = isbool;
    }
}
