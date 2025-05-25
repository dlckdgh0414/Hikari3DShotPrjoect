using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.Video;
using UnityEngine.UI;

public class StageLogic : MonoBehaviour
{
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
    private VideoPlayer video;

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
        Entity.IsGameStart = true;
        StartCoroutine(StartCutSceneRoutine());
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
        video.GetComponentInChildren<RawImage>().DOFade(0f, 2f);
    }

    public void ReturnMenuScene()
    {
        SceneManager.LoadScene("ShipStation");
    }

    public void ChangeAnimation(string newAnim)
    {
        Animator.Play(newAnim);
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
