using UnityEngine;

public class ExitUIState : MainMenuState, IMENUUILIB
{
    private void OnEnable()
    {
        OnUIEvent += UIEVENTHANDLER;
    }

    private void OnDisable()
    {
        OnUIEvent -= UIEVENTHANDLER;
    }

    public void UIEVENTHANDLER()
    {
        Debug.Log("게임 나가짐");
        Application.Quit();
    }
}