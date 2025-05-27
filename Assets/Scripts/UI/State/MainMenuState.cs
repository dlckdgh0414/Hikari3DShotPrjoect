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

    public void OnPointerDown(PointerEventData eventData)
    {
        OnUIEvent?.Invoke();
    }

    private void Awake()
    {
        originalScale = transform.localScale;
    }

    private void OnEnable()
    {
        originalScale = transform.localScale;
        transform.localScale = originalScale;
        isPointerOver = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isPointerOver) return;
        isPointerOver = true;

        // StartUIState는 애니메이션 안 쓰도록 예외 처리
        if (this is StartUIState)
            return;

        punchTween?.Kill();

        punchTween = transform.DOPunchScale(Vector3.one * size, dur, vid)
            .SetUpdate(true)
            .OnComplete(() =>
            {
                transform.localScale = originalScale;
            });
    }

  public void OnPointerExit(PointerEventData eventData)
{
    if (!isPointerOver) return;
    isPointerOver = false;

    if (this is StartUIState)
        return;

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