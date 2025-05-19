using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingManager : MonoBehaviour
{
    public static SettingManager Instance { get; private set; }

    [SerializeField] private GameObject[] _settingUiList = new GameObject[3];
    [SerializeField] private Player _player;

    public RectTransform mainUI;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        ForceShowCursor();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsSettingUIActive())
                CloseSetting();
            else
                OpenBaseSetting();
        }
    }

    public void OpenBaseSetting() => OpenSetting("Base");
    public void OpenSoundSetting() => OpenSetting("Sound");

    private void OpenSetting(string name)
    {
        ClearSetting();
        GameObject target = FindSetting(_settingUiList, name);

        if (target != null)
        {
            target.SetActive(true);
            StartCoroutine(WaitToPauseGame());
        }
    }

    private void CloseSetting()
    {
        ClearSetting();
        Time.timeScale = 1f;
        ForceShowCursor();
    }

    private void ClearSetting()
    {
        foreach (var ui in _settingUiList)
        {
            if (ui != null)
                ui.SetActive(false);
        }
    }

    private GameObject FindSetting(GameObject[] settingList, string name)
    {
        return settingList.FirstOrDefault(setting => setting != null && setting.name.Contains(name));
    }

    private bool IsSettingUIActive()
    {
        return _settingUiList.Any(ui => ui != null && ui.activeSelf);
    }

    private IEnumerator WaitToPauseGame()
    {
        yield return new WaitForSecondsRealtime(0.1f);

        ForceShowCursor(); 
        Time.timeScale = 0f;
    }

    private void ForceShowCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}

























//여기서부터가 옛날코드 중첩되는 코드가 많아서 버그가 발생할 가능성이 크기에 수정할때 위에코드 로직을 바꾸지말고 수정하시면 됩니다.
//using System.Collections;
//using System.Linq;
//using UnityEngine;
//using UnityEngine.SceneManagement;


//public class SettingManager : MonoBehaviour
//{
//    [SerializeField] private GameObject[] _settingUiList = new GameObject[3];

//    public RectTransform mainUI;

//    private bool _isOpenBase;

//    [SerializeField] private Player _player;

//    private float _currentPlayerSmoothSpeed;

//    private void Awake()
//    {
//        //SetUI();
//        if (SceneManager.GetActiveScene().name == "TitleScreen")
//        {
//            Cursor.visible = true;
//            Cursor.lockState = CursorLockMode.None;
//        }


//        DontDestroyOnLoad(gameObject);

//        _isOpenBase = true;

//        if (gameObject == null)
//        {
//            GameObject.FindAnyObjectByType<SettingManager>();
//            if (gameObject == null)
//            {
//                Instantiate(gameObject);
//            }
//        }

//        else
//            return;

//    }




//    public void OpenBaseSetting()
//    {
//        OpenSetting("Base");
//    }

//    public void OpenSoundSetting()
//    {
//        OpenSetting("Sound");
//    }

//    private void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.Escape))
//        {
//            if (_isOpenBase)
//            {
//                OpenBaseSetting();
//            }
//            else
//                CloseSetting();
//        }
//    }
//    private void SetUI()
//    {
//        _settingUiList.ToList().ForEach(UI => UI.transform.GetComponent<RectTransform>().sizeDelta
//        = new Vector2(1920,1080));
//    }


//    private void OpenSetting(string name)
//    {
//        ClearSetting();
//        FindSetting(_settingUiList, name).SetActive(true);
//        StartCoroutine(WaitClear());

//    }

//    private void ClearSetting()
//    {
//        _settingUiList.ToList().ForEach(setting => setting.SetActive(false));
//    }

//    private void CloseSetting()
//    {
//        ClearSetting();
//        Time.timeScale = 1f;
//        StartCoroutine(Clear());

//        if (SceneManager.GetActiveScene().name != "TitleScreen")
//        {
//            Cursor.visible = false;
//            Cursor.lockState = CursorLockMode.Locked;
//        }
//        else
//        {
//            Cursor.lockState = CursorLockMode.None;
//            Cursor.visible = true;
//        }

//    }

//    private GameObject FindSetting(GameObject[] settingList, string name)
//    {
//        foreach (GameObject setting in settingList)
//        {
//            if (setting != null && setting.name.Contains(name))
//                return setting;
//        }

//        return null;
//    }

//    private IEnumerator WaitClear()
//    {

//        yield return new WaitForSeconds(0.1f);
//        Cursor.lockState = CursorLockMode.None;
//        Cursor.visible = true;
//        _isOpenBase = false;
//        Time.timeScale = 0;
//    }

//    private IEnumerator Clear()
//    {
//        yield return new WaitForSeconds(0.5f);

//        _isOpenBase = true;
//    }

//    /*private void MoveSettingY(string name)
//    {
//        _uiInput.OnEscEvnet -= OpenBaseSetting;
//        CloseSetting();

//        MoveRectTransform(FindSetting(_settingUiList, name).GetComponent<RectTransform>(),
//            new Vector3(0, 0, 0), 0.3f);

//        StartCoroutine(WaitClear());
//    }


//    private void SetUIButton()
//    {

//    }


//    public void CloseSetting()
//    {
//        _uiInput.OnEscEvnet -= CloseSetting;

//        Time.timeScale = 1;

//        MoveRectTransform(_settingUiList[0].GetComponent<RectTransform>(), new Vector3(0,2000f, 0), 0.3f);
//        MoveRectTransform(_settingUiList[1].GetComponent<RectTransform>(), new Vector3(0,2000f, 0), 0.3f);

//        StartCoroutine(Clear());

//    }
//    private void MoveRectTransform(RectTransform rect, Vector3 destination, float duration)
//    {
//        StartCoroutine(MoveCoroutine(rect, destination, duration));
//    }

//    private IEnumerator WaitClear()
//    {
//        _uiInput.OnEscEvnet -= OpenBaseSetting;
//        yield return new WaitForSeconds(0.31f);

//        Time.timeScale = 0;

//        _uiInput.OnEscEvnet += CloseSetting;
//    }


//    private IEnumerator Clear()
//    {
//        yield return new WaitForSeconds(0.5f);

//        _uiInput.OnEscEvnet += OpenBaseSetting;
//    }


//    private IEnumerator MoveCoroutine(RectTransform rect, Vector3 destination, float duration)
//    {
//        Vector2 startPos = rect.anchoredPosition;
//        float time = 0f;

//        while (time < duration)
//        {
//            time += Time.deltaTime;

//            float t = Mathf.Clamp01(time / duration);

//            rect.anchoredPosition = Vector3.Slerp(startPos, destination, t);

//            yield return null;
//        }
//        rect.anchoredPosition = destination;
//    }*/

//}
