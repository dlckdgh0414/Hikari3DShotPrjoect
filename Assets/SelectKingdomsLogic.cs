using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;
using Ami.BroAudio;
using System.Collections;

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
    [SerializeField] private SoundID selectBGM;
    [SerializeField] private SoundID noSelectSkillSFX;
    Vector2 padding = new Vector2(0f, 30f);

    private void Awake()
    {
        Hide();
    }

    private void Start()
    {
        BroAudio.Play(selectBGM);
    }

    public void SetText(string text)
    {
        _text.SetText(text);               //�ؽ�Ʈ�� �Է�
        _text.ForceMeshUpdate();
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            PlayerSendInfo.Instance.ResetSkills();
            SceneManager.LoadScene("ShipStation");
        }
        _image.transform.position = (Vector2)Input.mousePosition + padding;
    }

    public void Show(string text)
    {
        _image.transform.position = (Vector2)Input.mousePosition + padding;
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
        BroAudio.Play(noSelectSkillSFX);
        _warningText.text = "��� ��ų�� ������ �ּ���.";
        _warningText.DOFade(1f, 0.5f).OnComplete(() => _warningText.DOFade(0f, 0.5f));
    }
    public void EqulsBind()
    {
        BroAudio.Play(noSelectSkillSFX);
        _warningText.text = "Ű�� �������ּ���.";
        _warningText.DOFade(1f, 0.5f).OnComplete(() => _warningText.DOFade(0f, 0.5f));
    }
    public void SceneStart()
    {
        Debug.Log("�� ��ų �� ��");
        if (PlayerSendInfo.Instance.DontSelectAllSkills())
            NoSelectSkillFadeTooltip();
        else if (KeyRebinder.isDuplicate)
            EqulsBind();
        else
            SceneManager.LoadScene(sceneNum);
    }
}
