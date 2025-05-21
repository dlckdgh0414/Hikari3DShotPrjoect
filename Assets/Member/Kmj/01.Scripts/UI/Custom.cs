using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Custom : MonoBehaviour
{
    [field: SerializeField]  public List<PlayerSkinSO> Skin = new List<PlayerSkinSO>();

    [field : SerializeField] public GameObject _playerSkin {get; private set;}

    [SerializeField] private ThisAirplaneType _airPlaneType;

    [SerializeField] public int currentMaterial = 0;

    private string path;

    private string skilPath;
    
    

    private void Start()
    {
        PlayerSendInfo.Instance.ThisSkill = Skin[currentMaterial];
        _airPlaneType.airplane.GetValueOrDefault(Skin[currentMaterial].name).SetActive(true);
    }

    public void NextMaterial()
    {
        currentMaterial++;
        if (currentMaterial >= Skin.Count)
            currentMaterial = 0;
        
        if (PlayerSendInfo.Instance.ThisSkill != null)
        {
            PlayerSendInfo.Instance.ThisSkill = null;
        }
        _airPlaneType.airplane.ToList().ForEach(plane => plane.Value.SetActive(false));
        _airPlaneType.airplane.GetValueOrDefault(Skin[currentMaterial].name).SetActive(true);
        
        PlayerSendInfo.Instance.ThisSkill = Skin[currentMaterial];
    }

    public void MinusMaterial()
    {
        currentMaterial--;

        if (currentMaterial < 0)
            currentMaterial = Skin.Count - 1;

        if (PlayerSendInfo.Instance.ThisSkill != null)
        {
            PlayerSendInfo.Instance.ThisSkill = null;
        }
        _airPlaneType.airplane.ToList().ForEach(plane => plane.Value.SetActive(false));
        _airPlaneType.airplane.GetValueOrDefault(Skin[currentMaterial].name).SetActive(true);
        
        PlayerSendInfo.Instance.ThisSkill = Skin[currentMaterial];
    }
}
 