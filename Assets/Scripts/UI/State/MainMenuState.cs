using UnityEngine;
using UnityEngine.EventSystems;
using System;
using DG.Tweening;

public class MainMenuState : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerExitHandler
{
    public Action OnUIEvent;
    public float size = 0.1f;
    public float dur = 0.2f;
    public int vid = 5;

    private bool isPointerOver = false;
    private Tween punchTween;

    private Vector3 originalScale;

    private void Awake()
    {
        originalScale = transform.localScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnUIEvent?.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isPointerOver) return;
        isPointerOver = true;


        punchTween?.Kill();


        originalScale = transform.localScale;

 
        punchTween = transform.DOPunchScale(originalScale * size, dur, vid)
            .SetUpdate(true)
            .OnComplete(() => {
                transform.localScale = originalScale;
            });
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isPointerOver) return;
        isPointerOver = false;

        punchTween?.Kill();
        transform.localScale = originalScale;
    }

    private void OnDisable()
    {
        punchTween?.Kill();
        transform.localScale = originalScale;
        isPointerOver = false;
    }
}