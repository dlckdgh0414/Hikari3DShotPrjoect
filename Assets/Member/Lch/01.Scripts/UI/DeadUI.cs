using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeadUI : MonoBehaviour
{
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button lobbyButton;

    private void Awake()
    {
        mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
        lobbyButton.onClick.AddListener(OnLobbyButtonClicked);
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
