using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup))]
public class DeadUI : MonoBehaviour
{
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button lobbyButton;
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
        lobbyButton.onClick.AddListener(OnLobbyButtonClicked);
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.interactable = false;
        _canvasGroup.alpha = 0f;
    }

    private void OnDestroy()
    {
        mainMenuButton.onClick.RemoveListener(OnMainMenuButtonClicked);
        lobbyButton.onClick.RemoveListener(OnLobbyButtonClicked);
    }

    public void OnDeadUIShow()
    {//너무 비효율 적이라 바꿈, 구르기 했을때 화면 휘는 것도 SO 이벤트로 만들었습니다
        DOTween.To(()=>_canvasGroup.alpha,x=>_canvasGroup.alpha=x,1f,0.2f).OnComplete(()=>
        {
            _canvasGroup.interactable = true;
        });
    }

    private void OnMainMenuButtonClicked()
    {
        Entity.IsGameStart = false;
        SceneManager.LoadScene("MainMenu");
    }
    private void OnLobbyButtonClicked()
    {
        Entity.IsGameStart = false;
        SceneManager.LoadScene("ShipStation");
    }

}
