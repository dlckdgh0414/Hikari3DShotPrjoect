using UnityEngine;

public class ExitUIState : MainMenuState, IMENUUILIB
{
    private void Awake()
    {
        OnUIEvent += UIEVENTHANDLER;
    }

    private void OnDestroy()
    {
        OnUIEvent -= UIEVENTHANDLER;
    }
    public void UIEVENTHANDLER()
    {
        Debug.Log("���� ������");
        Application.Quit();
    }
}
