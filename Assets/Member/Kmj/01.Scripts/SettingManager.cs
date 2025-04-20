using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SettingManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _settingUiList = new GameObject[3];

    public RectTransform mainUI;

    private bool _isOpenBase;

    [SerializeField] private Player _player;

    private float _currentPlayerSmoothSpeed;

    private void Awake()
    {
        //SetUI();
        if (SceneManager.GetActiveScene().name == "TitleScreen")
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }


        DontDestroyOnLoad(gameObject);

        _isOpenBase = true;

        if (gameObject == null)
        {
            GameObject.FindAnyObjectByType<SettingManager>();
            if (gameObject == null)
            {
                Instantiate(gameObject);
            }
        }

        else
            return;

    }




    public void OpenBaseSetting()
    {
        OpenSetting("Base");
    }

    public void OpenSoundSetting()
    {
        OpenSetting("Sound");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isOpenBase)
            {
                OpenBaseSetting();
            }
            else
                CloseSetting();
        }
    }
    private void SetUI()
    {
        _settingUiList.ToList().ForEach(UI => UI.transform.GetComponent<RectTransform>().sizeDelta
        = new Vector2(1920,1080));
    }


    private void OpenSetting(string name)
    {
        ClearSetting();
        FindSetting(_settingUiList, name).SetActive(true);
        StartCoroutine(WaitClear());

    }

    private void ClearSetting()
    {
        _settingUiList.ToList().ForEach(setting => setting.SetActive(false));
    }

    private void CloseSetting()
    {
        ClearSetting();
        Time.timeScale = 1f;
        StartCoroutine(Clear());

        if (SceneManager.GetActiveScene().name != "TitleScreen")
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

    }

    private GameObject FindSetting(GameObject[] settingList, string name)
    {
        foreach (GameObject setting in settingList)
        {
            if (setting != null && setting.name.Contains(name))
                return setting;
        }

        return null;
    }

    private IEnumerator WaitClear()
    {

        yield return new WaitForSeconds(0.1f);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        _isOpenBase = false;
        Time.timeScale = 0;
    }

    private IEnumerator Clear()
    {
        yield return new WaitForSeconds(0.5f);

        _isOpenBase = true;
    }

    /*private void MoveSettingY(string name)
    {
        _uiInput.OnEscEvnet -= OpenBaseSetting;
        CloseSetting();

        MoveRectTransform(FindSetting(_settingUiList, name).GetComponent<RectTransform>(),
            new Vector3(0, 0, 0), 0.3f);

        StartCoroutine(WaitClear());
    }


    private void SetUIButton()
    {
        
    }


    public void CloseSetting()
    {
        _uiInput.OnEscEvnet -= CloseSetting;

        Time.timeScale = 1;

        MoveRectTransform(_settingUiList[0].GetComponent<RectTransform>(), new Vector3(0,2000f, 0), 0.3f);
        MoveRectTransform(_settingUiList[1].GetComponent<RectTransform>(), new Vector3(0,2000f, 0), 0.3f);

        StartCoroutine(Clear());

    }
    private void MoveRectTransform(RectTransform rect, Vector3 destination, float duration)
    {
        StartCoroutine(MoveCoroutine(rect, destination, duration));
    }

    private IEnumerator WaitClear()
    {
        _uiInput.OnEscEvnet -= OpenBaseSetting;
        yield return new WaitForSeconds(0.31f);

        Time.timeScale = 0;

        _uiInput.OnEscEvnet += CloseSetting;
    }


    private IEnumerator Clear()
    {
        yield return new WaitForSeconds(0.5f);

        _uiInput.OnEscEvnet += OpenBaseSetting;
    }


    private IEnumerator MoveCoroutine(RectTransform rect, Vector3 destination, float duration)
    {
        Vector2 startPos = rect.anchoredPosition;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;

            float t = Mathf.Clamp01(time / duration);

            rect.anchoredPosition = Vector3.Slerp(startPos, destination, t);

            yield return null;
        }
        rect.anchoredPosition = destination;
    }*/

}
