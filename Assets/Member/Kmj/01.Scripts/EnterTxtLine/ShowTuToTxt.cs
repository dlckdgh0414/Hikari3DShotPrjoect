using DG.Tweening;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.VFX;

public class ShowTuToTxt : MonoBehaviour
{
    public Action OnTxtEvent;
    [SerializeField] private TextMeshProUGUI _txt;
    [SerializeField] private List<string> _txtList = new List<string>();
    [SerializeField] private List<Collider> _childColliderList;
    [SerializeField] private List<int> _fadeTimeList;

    private int _txtNum;

    private void Awake()
    {
        OnTxtEvent += InteractTxt;
        _txtNum = 0;
        GetColliderInChild();
    }

    public void InteractTxt()
    {
        _txt.alpha = 255;
        _txt.text = _txtList[_txtNum];
        _childColliderList[_txtNum].enabled = false;
        StartCoroutine(CloseTxt(_fadeTimeList[_txtNum]));
        _txtNum++;
    }


    private void GetColliderInChild()
    {
        foreach(Collider colider in GetComponentsInChildren<Collider>() )
        {
            _childColliderList.Add(colider);
        }
    }

    private IEnumerator CloseTxt(float sec)
    {
        yield return new WaitForSeconds(sec);

        _txt.DOFade(0, 3);

        yield return new WaitForSeconds(3f);

        _txt.DOKill();
    }
}
