using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using System;
using UnityEngine.SceneManagement;

public class KingdomButton : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler,IPointerClickHandler
{

    public Text text;
    public Image rect;
    public Image circle;
    public Action On_IPointer_World_LookAT_Event;
    public Action<string> WorldName;
    public Color textColorWhenSelected;
    public Color rectColorMouseOver;
    public string WoldName;
    private bool IsTweening = false;
    private bool hasTweened = false; // 트윈이 실행되었는지 여부

    //맵 일치하는지 확인(보안용도및 버그 가능성 베제)

    private Dictionary<string, int> worldSceneMap = new Dictionary<string, int>()
    {
        { "World 1", 2 },
        { "World 2", 3 },
        { "World 3", 4 },
        { "World 4", 5 }
    };
    private void Start()
    {
        rect.color = Color.clear;
        text.color = Color.white;
        circle.color = Color.white;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!hasTweened && !IsTweening) 
        {
            IsTweening = true;

       
            rect.DOColor(Color.white, .1f);
            text.DOColor(textColorWhenSelected, .1f);
            circle.DOColor(Color.red, .1f);

            rect.transform.DOComplete();
            rect.transform.DOPunchScale(Vector3.one / 3, .2f, 20, 1);
            IsTweening = false;
            hasTweened = true; 
        }

        On_IPointer_World_LookAT_Event?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!IsTweening)
        {
            IsTweening = true;

          
            rect.DOColor(Color.clear, .1f);
            text.DOColor(Color.white, .1f);
            circle.DOColor(Color.white, .1f).OnComplete(() => IsTweening = false);
            
           

            hasTweened = false;
         
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        WoldName = transform.GetChild(0).GetChild(0).GetComponent<Text>().text;
        
        Debug.Log(WoldName);
        if (worldSceneMap.TryGetValue(WoldName, out int sceneName))
        {
            WorldName?.Invoke(WoldName);
            SceneManager.LoadScene(sceneName);
        }

        else
        {
            Debug.LogWarning("대상이 없습니다. ==worldScene== 키값이 잘못됐거나 씬의 이름이 일치하는지 확인하면됨");
        }
    }

   

}
