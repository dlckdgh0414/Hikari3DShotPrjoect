using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using DG.Tweening;

public class StageLogic : MonoBehaviour
{
    [SerializeField]
    private GameObject enemySpawner;
    [SerializeField]
    private GameObject mapCreator;
    [SerializeField]
    private GameObject map;
    private Player _player;

    [SerializeField]
    private Animator Animator;

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
    }
}
