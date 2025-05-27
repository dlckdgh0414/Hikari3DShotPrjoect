using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.Playables;

public class StartUIState : MainMenuState, IMENUUILIB
{


    private CanvasGroup group;
    private PlayableDirector director;
    private bool hasStarted = false;
    private void Awake()
    {
        // 필요한 컴포넌트 캐싱
        group = GetComponentInParent<CanvasGroup>();
        director = GetComponentInParent<PlayableDirector>();

        // 이벤트 구독은 OnEnable에서 하는 게 안전함
    }

    private void OnEnable()
    {
        OnUIEvent += UIEVENTHANDLER;
        hasStarted = false;

        if (group != null)
            group.alpha = 1f;
    }

    private void OnDisable()
    {
        OnUIEvent -= UIEVENTHANDLER;
    }

    public void UIEVENTHANDLER()
    {
        if (hasStarted) return;
        hasStarted = true;
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {


      

        if (director != null)
            director.Play();

        if (group != null)
            DOTween.To(() => group.alpha, x => group.alpha = x, 0, 0.2f);

        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene("SpaceShip");
    }
}