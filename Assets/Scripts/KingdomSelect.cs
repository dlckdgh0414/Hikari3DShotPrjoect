using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;
using System.Collections;

public class KingdomSelect : MonoBehaviour
{
    public List<Kingdom> kingdoms = new List<Kingdom>();
    public Action<Kingdom> OnChooseWord;

    [Header("Public References")]
    public GameObject kingdomPointPrefab;
    public GameObject kingdomButtonPrefab;
    public Transform modelTransform;
    public Transform kingdomButtonsContainer;
    private KingdomButton btn;
    [Header("Tween Settings")]
    public float lookDuration;
    public Ease lookEase;

    public Vector2 visualOffset;
    private string WorldName;

    private static int a = 0;

    void Start()
    {
        SaveData data = SaveSystem.Load(0);
        if (data != null)
        {
            Debug.Log("값있음");
            a = data.World;

            Debug.Log(a);
        }
        else
        {
            Debug.Log("값없음");
            a = 1; // 최초 실행
            _WorldName(a); // 초기값 저장
        }

        foreach (Kingdom k in kingdoms)
        {
            SpawnKingdomPoint(k);
        }

        if (kingdoms.Count > 0)
        {
            LookAtKingdom(kingdoms[0]);
            UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(kingdomButtonsContainer.GetChild(0).gameObject);

        }

        for (int i = 1; i < kingdoms.Count; i++)
        {
            kingdomButtonsContainer.GetChild(i).gameObject.SetActive(false);
        }



        if (ClearGame.IsCLEAR)
        {
            _ClearGame();
            ClearGame.IsCLEAR = false;
        }

        for (int i = 0; i < a; i++)
        {
            kingdomButtonsContainer.GetChild(i).gameObject.SetActive(true);
        }


    }

    private void SpawnKingdomPoint(Kingdom k)
    {
        GameObject kingdom = Instantiate(kingdomPointPrefab, modelTransform);
        kingdom.transform.localEulerAngles = new Vector3(k.y + visualOffset.y, -k.x - visualOffset.x, 0);
        k.visualPoint = kingdom.transform.GetChild(0);
        SpawnKingdomButton(k);
    }

    private void SpawnKingdomButton(Kingdom k)
    {
        GameObject obj = Instantiate(kingdomButtonPrefab, kingdomButtonsContainer);
        Button kingdomButton = obj.GetComponent<Button>();
        KingdomButton king = obj.GetComponent<KingdomButton>();

        if (king != null)
        {
            king.On_IPointer_World_LookAT_Event += () => LookAtKingdom(k);
        }
        else
        {
            Debug.LogWarning("KingdomButton 컴포넌트가 없음!");
        }

        kingdomButton.transform.GetChild(0).GetComponentInChildren<Text>().text = k.name;
        btn = king;
       
    }

    public void LookAtKingdom(Kingdom k)
    {
        Transform cameraParent = Camera.main.transform.parent;
        Transform cameraPivot = cameraParent.parent;

        cameraParent.DOLocalRotate(new Vector3(k.y, 0, 0), lookDuration, RotateMode.Fast).SetEase(lookEase);
        cameraPivot.DOLocalRotate(new Vector3(0, -k.x, 0), lookDuration, RotateMode.Fast).SetEase(lookEase);

        FollowTarget followTarget = FindFirstObjectByType<FollowTarget>();
        if (followTarget != null)
        {
            followTarget.target = k.visualPoint;
        }
        else
        {
            Debug.LogWarning("FollowTarget을 찾을 수 없음!");
        }
    }

    private void _ClearGame()
    {
        ++a;
        Debug.Log(a);
        Debug.Log("이벤트 즉각 발행됨");
        StartCoroutine(_ClearGameCo());
    }

    private IEnumerator _ClearGameCo()
    {
        Debug.Log(a);
        yield return new WaitForSeconds(1f);

        if (kingdomButtonsContainer.childCount > 0)
        {
            for (int i = 1; i < a; i++)
            {
                kingdomButtonsContainer.GetChild(i).gameObject.SetActive(true);
            }
            
            _WorldName(a);
        }
        else
        {
            Debug.LogWarning("오브젝트가 아직 생성되지 않음 또는 kingdomButtonsContainer가 없음.");
        }
    }

    private void _WorldName(int worldseed)
    {
        Debug.Log($"현재 이름:{worldseed}");
        SaveData World = new SaveData(worldseed);
        SaveSystem.Save(World, 0); // 항상 0번 슬롯에 저장
    }

    private void OnDestroy()
    {
        
    }

    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        Gizmos.color = Color.red;

        if (kingdoms.Count > 0)
        {
            foreach (var k in kingdoms)
            {
                Vector3 pointPosition = Quaternion.Euler(k.y + visualOffset.y, -k.x - visualOffset.x, 0) * new Vector3(0, 0, -0.5f);

                if (!Application.isPlaying)
                {
                    Gizmos.DrawWireSphere(pointPosition, 0.02f);
                }

                Gizmos.DrawSphere(pointPosition, 0.07f);
            }
        }
#endif
    }
}

[System.Serializable]
public class Kingdom
{
    public string name;
    [Range(-180, 180)] public float x;
    [Range(-89, 89)] public float y;
    [HideInInspector] public Transform visualPoint;
}