using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.Playables;

public class StageLogic : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector _director;

    [SerializeField]
    private GameObject enemySpawner;
    [SerializeField]
    private GameObject mapCreator;
    [SerializeField]
    private GameObject map;
    [SerializeField]
    private GameObject grassMap;
    private Player _player;

    [SerializeField]
    private Animator Animator;

    [SerializeField]
    private bool isCutScene;

    [SerializeField]
    private VideoPlayer video;

    [SerializeField] private GameEventChannelSO CameraChannel;

    private void Awake()
    {
        _player = FindAnyObjectByType<Player>();
    }
    private void Start()
    {
        Animator.Play("Floating");
        _player.zPos = -3.6f;

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void GameStart()
    {
        map.transform.DOMoveZ(-300f,4f);
        _player.SetZpos(7.2f,5f);
        if (isCutScene) StartCoroutine(BossCutScene());
        else
            RealStageStart();
    }

    public void RealStageStart()
    {
        Entity.IsGameStart = true;
        StartCoroutine(StartCutSceneRoutine());
    }
    public void ZoomBoss(float value)
    {
        CameraEffectEvent effectCamera = CamaraEvents.CameraEffectEvent;
        effectCamera.cameraEffect = CameraEffectEnum.FOV;
        effectCamera.second = 1f;
        effectCamera.value = value;
        effectCamera.effectEase = Ease.InOutQuad;

        CameraChannel.RaiseEvent(effectCamera);
    }
    private IEnumerator BossCutScene()
    {
        yield return new WaitForSeconds(3f);
        _director.Play();
    }



    public void GrassChange()
    {
        StartCoroutine(WaitRoutine());
    }
    private IEnumerator WaitRoutine()
    {
        video.Play();
        yield return new WaitForSeconds(4f);
        video.GetComponentInChildren<RawImage>().DOFade(0.9f, 2f);
        yield return new WaitForSeconds(9f);
        grassMap.SetActive(true);
        mapCreator.SetActive(false);
        video.GetComponentInChildren<RawImage>().DOFade(0f, 1f);
    }

    public void ReturnMenuScene()
    {
        ClearGame.instance.ClearMethod();
        SceneManager.LoadScene("ShipStation");
    }


    private IEnumerator StartCutSceneRoutine()
    {
        yield return new WaitForSeconds(3f);
        map.SetActive(false);
        _player.SetGameUI(1f,Ease.InOutQuad);
        enemySpawner.SetActive(true);
        mapCreator.SetActive(true);
    }
}
