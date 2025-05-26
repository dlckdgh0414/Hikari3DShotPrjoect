using NUnit.Framework;
using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameStartButton : MonoBehaviour
{
    [SerializeField] private GameObject[] _settingUiList;

    public RectTransform mainUI;

    private bool _isOpenBase;


    private float _currentPlayerSmoothSpeed;

    private void Awake()
    {
        //SetUI();
        DontDestroyOnLoad(gameObject);

        _isOpenBase = true;

        if (gameObject == null)
        {
            GameObject.FindAnyObjectByType<GameStartButton>();
            if (gameObject == null)
            {
                Instantiate(gameObject);
            }
        }
        else
            return;

        _settingUiList.ToList().ForEach(UI => Debug.Log(UI.name));
    }
    public void OpenCustomBtn()
    {
        OpenSetting("Custom");
    }

    public void OpenSkillTree()
    {
        OpenSetting("SkillTree");
    }

    public void OpenSelectSkill()
    {
        OpenSetting("SelectSkill");
    }

    private void Update()
    {
    }
    private void SetUI()
    {
        _settingUiList.ToList().ForEach(UI => UI.transform.GetComponent<RectTransform>().sizeDelta
        = new Vector2(1920, 1080));
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
    }

    private IEnumerator Clear()
    {
        yield return new WaitForSeconds(0.5f);

        _isOpenBase = true;
    }
}
