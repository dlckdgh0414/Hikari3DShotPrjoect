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

        // Tween�� ��� ������ ����
        punchTween?.Kill();
        punchTween = null;
    }

    public void UIEVENTHANDLER()
    {
        if (SettingObj == null) return;

        // ���� Ȱ��ȭ������� DOTween�� �۵���
        SettingObj.SetActive(true);

        // ������ �ʱ�ȭ �� Ʈ�� ����
        Transform settingTransform = SettingObj.transform;
        settingTransform.localScale = Vector3.one;

        punchTween?.Kill(); // �ߺ� ����
        punchTween = settingTransform.DOPunchScale(Vector3.one * 0.2f, 0.5f, 10, 1)
            .SetUpdate(true)
            .OnComplete(() =>
            {
                settingTransform.localScale = Vector3.one;
            });
    }
}