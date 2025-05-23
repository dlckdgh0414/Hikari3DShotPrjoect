using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class StageLogic : MonoBehaviour
{
    [SerializeField]
    private GameObject enemySpawner;
    private Player _player;

    private void Awake()
    {
        _player = FindAnyObjectByType<Player>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Entity.IsGameStart = true;
        StartCoroutine(StartCutSceneRoutine());
    }

    private IEnumerator StartCutSceneRoutine()
    {
        yield return new WaitForSeconds(3f);


        _player.SetGameUI(true);
        enemySpawner.SetActive(true);
    }
}
