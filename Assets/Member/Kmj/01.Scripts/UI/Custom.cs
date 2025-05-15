using System;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Custom : MonoBehaviour
{
    [field: SerializeField]  public List<PlayerSkinSO> Skin = new List<PlayerSkinSO>();

    [field : SerializeField] public GameObject _playerSkin {get; private set;}

    [SerializeField] public int currentMaterial;

    private string path;

    private string skilPath;
    

    private void Awake()
    {
        path = AssetDatabase.GetAssetPath(_playerSkin);
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
        
        PlayerSendInfo.Instance.ThisSkill = Skin[currentMaterial];
    }
}
 