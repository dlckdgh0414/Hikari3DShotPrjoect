using UnityEngine;

public class SettingUIState : MainMenuState, IMENUUILIB
{

    private void Awake()
    {
        OnUIEvent += UIEVENTHANDLER;
    }
    public void UIEVENTHANDLER()
    {
        Debug.Log("���� �����");
    }
}
