using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Custom : MonoBehaviour
{
    //[field : SerializeField] public GameObject _playerSkin {get; private set;}

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
        
        if (skinListSO.invenSkillList[currentMaterial].name == "BigSparrow")
        {
            _planeTxt.text = "빅 스패로우"; 
        }
        else if (skinListSO.invenSkillList[currentMaterial].name == "BombardiroCrocodilo")
        {
            _planeTxt.text = "봄바르딜로 크로코딜로";
        }
        else if (skinListSO.invenSkillList[currentMaterial].name == "Defalut")
        {
            _planeTxt.text = "기본"; 
        }
        else if (skinListSO.invenSkillList[currentMaterial].name == "Flyer")
        {
            _planeTxt.text = "파리"; 
        }
        else if (skinListSO.invenSkillList[currentMaterial].name == "ManyMuzzle")
        {
            _planeTxt.text = "메니머질"; 
        }
        else if (skinListSO.invenSkillList[currentMaterial].name == "Miner")
        {
            _planeTxt.text = "마이너"; 
        }
        else if (skinListSO.invenSkillList[currentMaterial].name == "Black")
        {
            _planeTxt.text = "블랙"; 
        }
        else if (skinListSO.invenSkillList[currentMaterial].name == "Mint")
        {
            _planeTxt.text = "민트"; 
        }
        else if (skinListSO.invenSkillList[currentMaterial].name == "SpeedSparrow")
        {
            _planeTxt.text = "스피드 스패로우"; 
        }
        else if (skinListSO.invenSkillList[currentMaterial].name == "Grin")
        {
            _planeTxt.text = "그린 스패로우"; 
        }
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
        
        if (skinListSO.invenSkillList[currentMaterial].name == "BigSparrow")
        {
            _planeTxt.text = "빅 스패로우"; 
        }
        else if (skinListSO.invenSkillList[currentMaterial].name == "BombardiroCrocodilo")
        {
            _planeTxt.text = "붐바르딜로 크로커딜로"; 
        }
        else if (skinListSO.invenSkillList[currentMaterial].name == "Defalut")
        {
            _planeTxt.text = "기본"; 
        }
        else if (skinListSO.invenSkillList[currentMaterial].name == "Flyer")
        {
            _planeTxt.text = "파리"; 
        }
        else if (skinListSO.invenSkillList[currentMaterial].name == "ManyMuzzle")
        {
            _planeTxt.text = "메니머질"; 
        }
        else if (skinListSO.invenSkillList[currentMaterial].name == "Miner")
        {
            _planeTxt.text = "마이너"; 
        }
        else if (skinListSO.invenSkillList[currentMaterial].name == "Black")
        {
            _planeTxt.text = "블랙"; 
        }
        else if (skinListSO.invenSkillList[currentMaterial].name == "Mint")
        {
            _planeTxt.text = "민트"; 
        }
        else if (skinListSO.invenSkillList[currentMaterial].name == "SpeedSparrow")
        {
            _planeTxt.text = "스피드 스패로우"; 
        }
        else if (skinListSO.invenSkillList[currentMaterial].name == "Grin")
        {
            _planeTxt.text = "그린 스패로우"; 
        }
        
        

    
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
        if (skinListSO.invenSkillList[currentMaterial].name == "BigSparrow")
        {
            _planeTxt.text = "빅 스패로우"; 
        }
        else if (skinListSO.invenSkillList[currentMaterial].name == "BombardiroCrocodilo")
        {
            _planeTxt.text = "붐바르딜로 크로커딜로"; 
        }
        else if (skinListSO.invenSkillList[currentMaterial].name == "Defalut")
        {
            _planeTxt.text = "기본"; 
        }
        else if (skinListSO.invenSkillList[currentMaterial].name == "Flyer")
        {
            _planeTxt.text = "파리"; 
        }
        else if (skinListSO.invenSkillList[currentMaterial].name == "ManyMuzzle")
        {
            _planeTxt.text = "메니머질"; 
        }
        else if (skinListSO.invenSkillList[currentMaterial].name == "Miner")
        {
            _planeTxt.text = "마이너"; 
        }
        else if (skinListSO.invenSkillList[currentMaterial].name == "Black")
        {
            _planeTxt.text = "블랙"; 
        }
        else if (skinListSO.invenSkillList[currentMaterial].name == "Mint")
        {
            _planeTxt.text = "민트"; 
        }
        else if (skinListSO.invenSkillList[currentMaterial].name == "SpeedSparrow")
        {
            _planeTxt.text = "스피드 스패로우"; 
        }
        else if (skinListSO.invenSkillList[currentMaterial].name == "Grin")
        {
            _planeTxt.text = "그린 스패로우"; 
        }
    }
}
 