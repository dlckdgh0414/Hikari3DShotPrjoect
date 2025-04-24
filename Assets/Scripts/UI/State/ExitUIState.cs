using UnityEngine;

public class ExitUIState : MainMenuState, IMENUUILIB
{
    private void Awake()
    {
        OnUIEvent += UIEVENTHANDLER;
    }


    public void UIEVENTHANDLER()
    {
        Debug.Log("게임 나가짐");
        Application.Quit();
    }
}
