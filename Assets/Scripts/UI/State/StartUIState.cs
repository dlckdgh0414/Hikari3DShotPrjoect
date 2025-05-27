using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.Playables;

public class StartUIState : MainMenuState, IMENUUILIB
{
    [SerializeField] UIDissolveEffect1 uds;

    private CanvasGroup group;
    private PlayableDirector director;
    private bool hasStarted = false;
    [SerializeField] GameObject obj;
    private void Awake()
    {
        // �ʿ��� ������Ʈ ĳ��
        group = GetComponentInParent<CanvasGroup>();
        director = GetComponentInParent<PlayableDirector>();

        // �̺�Ʈ ������ OnEnable���� �ϴ� �� ������
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


        obj.SetActive(true);

        if (director != null)
            director.Play();

        if (group != null)
            DOTween.To(() => group.alpha, x => group.alpha = x, 0, 0.2f);

        yield return new WaitForSeconds(1f);
      

        uds.ShowUIEffect();
        yield return new WaitForSeconds(4f);

        SceneManager.LoadScene("SpaceShip");
    }
}