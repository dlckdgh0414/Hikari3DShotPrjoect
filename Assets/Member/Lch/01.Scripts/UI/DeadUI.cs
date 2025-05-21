using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeadUI : MonoBehaviour
{
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button lobbyButton;
    [SerializeField] private GameEventChannelSO deadUIChannel;

    private void Awake()
    {
        mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
        lobbyButton.onClick.AddListener(OnLobbyButtonClicked);
        gameObject.SetActive(false);
        deadUIChannel.AddListener<DeadEvent>(OnDeadUIShow);
    }

    private void OnDestroy()
    {
        deadUIChannel.RemoveListener<DeadEvent>(OnDeadUIShow);
    }

    private void OnDeadUIShow(DeadEvent evt)
    {
        gameObject.SetActive(evt.isDead);
    }

    private void OnMainMenuButtonClicked()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    private void OnLobbyButtonClicked()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

}
