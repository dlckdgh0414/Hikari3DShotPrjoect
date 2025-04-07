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
        Destroy(gameObject); // 중복이면 제거
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
        Debug.Log("어 제데로 인보크 되고있음 ㅄ아");
    }

    private void OnDisable()
    {
     
        Debug.Log("삭제잘됨 ㅄ아");
    }
}
