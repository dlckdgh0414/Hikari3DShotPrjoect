using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.Playables;

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
    
    private void OnDestroy()
    {
        OnUIEvent -= UIEVENTHANDLER;
    }

    IEnumerator StartGame()
    {
        uds.ShowUIEffect();
        InfinityFloatMotion.AnimaStop(true);
        GetComponentInParent<PlayableDirector>().Play();
        CanvasGroup group = GetComponentInParent<CanvasGroup>();
        DOTween.To(()=>group.alpha,x=>group.alpha=x,0,0.2f);
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("SpaceShip");
    }
}
