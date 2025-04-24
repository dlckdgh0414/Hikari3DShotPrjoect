using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SceenScale : MonoBehaviour
{
    public TMP_Dropdown dropDown;

    public Toggle fullScreenToggle;

    private Resolution[] _resol;

    private bool _isFullScreen;

    private int _selectResol;

    private List<Resolution> _resolList = new List<Resolution>();

    [SerializeField] private TextMeshProUGUI _resText;


    private void Start()
    {
        _isFullScreen = true;
        _resol = Screen.resolutions;

        List<string> resolustion = new List<string>();

        string newRes;

        foreach(Resolution r in _resol)
        {

            newRes = r.width.ToString() + " x " + r.height.ToString();
            if(!resolustion.Contains(newRes))
            {
                resolustion.Add(newRes);
                _resolList.Add(r);
            }
        }

        resolustion.Reverse();


        dropDown.AddOptions(resolustion);

    }

    public void ChangeResolution()
    {
        _selectResol = dropDown.value;
        Screen.SetResolution(_resolList[_selectResol].width, _resolList[_selectResol].height, _isFullScreen);
    }

    public void ChangeFullScreen()
    {
        _isFullScreen = fullScreenToggle.isOn;

        Screen.SetResolution(_resolList[_selectResol].width, _resolList[_selectResol].height, _isFullScreen);
    }
}
