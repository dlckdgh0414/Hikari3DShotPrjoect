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
        Debug.Log("���� ������");
        Application.Quit();
    }
}