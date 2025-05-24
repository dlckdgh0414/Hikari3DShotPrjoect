using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;

public class SelectKingdomsLogic : MonoBehaviour
{
    [SerializeField]
    private GameObject skillSelectUI;
    private int sceneNum;
    [SerializeField]
    private GameObject ui;
    [SerializeField]
    private TextMeshProUGUI _text;
    [SerializeField]
    private TextMeshProUGUI _warningText;
    [SerializeField]
    private Image _image;

    private void Awake()
    {
        Hide();
    }

    public void SetText(string text)
    {
        _text.SetText(text);               //텍스트를 입력
        _text.ForceMeshUpdate();

        Vector2 textSize = _text.GetRenderedValues(false);   //띄어쓰기도 포함된 (렌더링 된) 텍스트의 너비
        Vector2 offset = new Vector2(-4f, -4f); //여백의 크기
        _image.transform.GetComponent<RectTransform>().sizeDelta = textSize + offset;
    }

    private void Update()
    {
        _image.transform.position = Input.mousePosition;
    }

    public void Show(string text)
    {
        _image.transform.position = Input.mousePosition;
        _image.gameObject.SetActive(true);
        SetText(text);
    }

    public void Hide()
    {
        _image.gameObject.SetActive(false);
    }

    public void SceneSetting(int num)
    {
        sceneNum = num;
        skillSelectUI.SetActive(true);
        ui.gameObject.SetActive(false);
    }
    public void NoSelectSkillFadeTooltip()
    {
        _warningText.DOFade(1f, 0.5f).OnComplete(() => _warningText.DOFade(0f, 0.5f));
    }
    public void SceneStart()
    {
        Debug.Log("님 스킬 안 고름");
        if (PlayerSendInfo.Instance.CanStart())
            SceneManager.LoadScene(sceneNum);
        else
            NoSelectSkillFadeTooltip();
    }
}
