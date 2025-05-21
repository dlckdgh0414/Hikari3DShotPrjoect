using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Custom : MonoBehaviour
{
    [field : SerializeField] public GameObject _playerSkin {get; private set;}

    [SerializeField] private ThisAirplaneType _airPlaneType;

    [SerializeField] private UseSkillDataSO skinListSO;
    [SerializeField] public int currentMaterial = 0;
    [SerializeField] private TextMeshProUGUI _planeTxt;
    private string path;

    private string skilPath;
    
    

    private void Start()
    {
        PlayerSendInfo.Instance.ThisSkill = skinListSO.invenSkillList[currentMaterial];
        _airPlaneType.airplane.ToList().ForEach(plane => plane.Value.SetActive(false));
        _airPlaneType.airplane.GetValueOrDefault(skinListSO.invenSkillList[currentMaterial].name).SetActive(true);
        _planeTxt.text = skinListSO.invenSkillList[currentMaterial].name;
    }

    public void NextMaterial()
    {
        currentMaterial++;
        if (currentMaterial >= skinListSO.invenSkillList.Count)
            currentMaterial = 0;
        
        if (PlayerSendInfo.Instance.ThisSkill != null)
        {
            PlayerSendInfo.Instance.ThisSkill = null;
        }
        _airPlaneType.airplane.ToList().ForEach(plane => plane.Value.SetActive(false));
        _airPlaneType.airplane.GetValueOrDefault(skinListSO.invenSkillList[currentMaterial].name).SetActive(true);
        
        PlayerSendInfo.Instance.ThisSkill = skinListSO.invenSkillList[currentMaterial];
        _planeTxt.text = skinListSO.invenSkillList[currentMaterial].name;
    }

    public void MinusMaterial()
    {
        currentMaterial--;

        if (currentMaterial < 0)
            currentMaterial = skinListSO.invenSkillList.Count - 1;

        if (PlayerSendInfo.Instance.ThisSkill != null)
        {
            PlayerSendInfo.Instance.ThisSkill = null;
        }
        _airPlaneType.airplane.ToList().ForEach(plane => plane.Value.SetActive(false));
        _airPlaneType.airplane.GetValueOrDefault(skinListSO.invenSkillList[currentMaterial].name).SetActive(true);
        
        PlayerSendInfo.Instance.ThisSkill = skinListSO.invenSkillList[currentMaterial];
        _planeTxt.text = skinListSO.invenSkillList[currentMaterial].name;
    }
}
 