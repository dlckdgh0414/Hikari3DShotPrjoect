using DG.Tweening;
using UnityEngine;

public class SettingUIState : MainMenuState, IMENUUILIB
{
    [SerializeField] private GameObject SettingObj = null;
    private void Awake()
    {
        OnUIEvent += UIEVENTHANDLER;
    }
    public void UIEVENTHANDLER()
    {
        SettingObj.transform.DOPunchScale(SettingObj.transform.localScale * 0.2f, 0.5f, 10, 1);
        SettingObj.SetActive(true);
    }


}
