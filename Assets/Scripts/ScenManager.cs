using System;
using System.Collections;
using UnityEngine;

public class ScenManager : MonoBehaviour
{
  public static  ScenManager instance;
    public Action Oncomplete;
    [SerializeField] private scenSO scenComplete;
    private void OnEnable()
    {
       if (instance != null && instance != this)
    {
        Destroy(gameObject); // �ߺ��̸� ����
        return;
    }

    instance = this;
    DontDestroyOnLoad(gameObject);
        
       
        scenComplete.OnScenEvent += _ClearGame;
    
    }
    private void _ClearGame(bool isClear)
    {
      
        if (isClear)
        {
            isClear = false;
            StartCoroutine(ClearGameCO());
        }
    }

   private IEnumerator ClearGameCO()
    {
        yield return new WaitForSeconds(1f);
        Oncomplete?.Invoke();
        Debug.Log("�� ������ �κ�ũ �ǰ����� ����");
    }

    private void OnDisable()
    {
     
        Debug.Log("�����ߵ� ����");
    }
}
