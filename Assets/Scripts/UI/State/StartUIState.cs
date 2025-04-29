using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;
public class StartUIState : MainMenuState,IMENUUILIB
{
    [SerializeField] UIDissolveEffect1 uds;
    private void Awake()
    {
        OnUIEvent += UIEVENTHANDLER;
    }
    public void UIEVENTHANDLER()
    {
        StartCoroutine(StartGame());
       
    }

   IEnumerator StartGame()
    {
        uds.ShowUIEffect();
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(0);
    }
}
