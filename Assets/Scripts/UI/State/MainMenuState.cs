using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System;
using DG.Tweening;
public class MainMenuState : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerExitHandler
{
    public Action OnUIEvent;
    public float size;
    public float dur;
    public int vid;
    private static bool flag;

    private Vector3 Oldsize;

    public void OnPointerDown(PointerEventData eventData)
    {
        OnUIEvent?.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (flag)
        {
            flag = false;
            Oldsize = gameObject.transform.localScale;
            gameObject.transform.DOPunchScale(transform.localScale * size, dur, vid);
        }
      
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!flag)
        {
            flag = true;
            Oldsize = gameObject.transform.localScale;
            gameObject.transform.localScale = Oldsize;
         
        }
    }

    public void OnDisable()
    {
        gameObject.transform.DOKill();
    }
}
