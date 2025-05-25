
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
            Destroy(gameObject); // �ߺ��̸� ����
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

    }
    private void Update()
    { // �� ��ũ��Ʈ ���� �ƹ� ��ü�� �����ְ� ClearMethod ����ϸ��
      // ���� ������Ʈ ���� �� �׽�Ʈ���̴ϱ� ������ ��.
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
            StartCoroutine(ClearCO());      // 1������ ��ٸ� (�� ��ȯ ������)
            // �� �� Raise
        }
    }

  IEnumerator ClearCO()
    {
        SceneManager.LoadScene(0); // �� ����
        yield return new WaitForSecondsRealtime(1f);
        scenOnComplete.Raise();
        Debug.Log("Ŭ�����");
    }


}
