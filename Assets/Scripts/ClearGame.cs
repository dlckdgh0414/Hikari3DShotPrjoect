
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearGame : MonoBehaviour
{
    public bool isClear = false;
    [SerializeField] scenSO scenOnComplete;
    public static ClearGame instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // 중복이면 제거
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

    }
    private void Update()
    { // 이 스크립트 대충 아무 객체에 때려넣고 ClearMethod 사용하면됨
      // 여기 업데이트 문은 걍 테스트용이니까 지워도 돼.
        if (Input.GetKeyDown(KeyCode.F))
        {
            isClear = true;
            ClearMethod();
        }
    }

    public void ClearMethod()
    {
        if (isClear)
        {
            
            isClear = false;
            StartCoroutine(ClearCO());      // 1프레임 기다림 (씬 전환 마무리)
            // 그 후 Raise
        }
    }

  IEnumerator ClearCO()
    {
        SceneManager.LoadScene(0); // 씬 변경
        yield return new WaitForSecondsRealtime(1f);
        scenOnComplete.Raise();
        Debug.Log("클리어됨");
    }


}
