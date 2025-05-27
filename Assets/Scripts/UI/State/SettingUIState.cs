using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class SettingUIState : MainMenuState, IMENUUILIB
{
    [SerializeField] private GameObject SettingObj = null;
    private Tween punchTween;

    private void OnEnable()
    {
        OnUIEvent += UIEVENTHANDLER;
    }

    private void OnDisable()
    {
        OnUIEvent -= UIEVENTHANDLER;

        // Tween이 살아 있으면 종료
        punchTween?.Kill();
        punchTween = null;
    }

    public void UIEVENTHANDLER()
    {
        if (SettingObj == null) return;

        // 먼저 활성화시켜줘야 DOTween이 작동함
        SettingObj.SetActive(true);

        // 스케일 초기화 후 트윈 적용
        Transform settingTransform = SettingObj.transform;
        settingTransform.localScale = Vector3.one;

        punchTween?.Kill(); // 중복 방지
        punchTween = settingTransform.DOPunchScale(Vector3.one * 0.2f, 0.5f, 10, 1)
            .SetUpdate(true)
            .OnComplete(() =>
            {
                settingTransform.localScale = Vector3.one;
            });
    }
}