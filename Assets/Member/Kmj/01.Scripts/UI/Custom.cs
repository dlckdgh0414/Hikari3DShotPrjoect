using System;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Custom : MonoBehaviour
{
    [SerializeField]  private List<PlayerSkinSO> Skin = new List<PlayerSkinSO>();

    [field : SerializeField] public GameObject _playerSkin {get; private set;}

    [SerializeField] private int currentMaterial;

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

        skilPath = AssetDatabase.GetAssetPath(Skin[currentMaterial]);
        
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
        if (prefab == null)
        {
            return;
        }
                
        GameObject instance = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
                
        ModelChanger Changer = instance.GetComponentInChildren<ModelChanger>();
        if (Changer == null)
        {
            return;
        }
        
        PlayerSkinSO SO = AssetDatabase.LoadAssetAtPath<PlayerSkinSO>(skilPath);

        Changer.skinSO = SO;
                
        PrefabUtility.SaveAsPrefabAsset(instance, path);
        GameObject.DestroyImmediate(instance);


        Debug.Log("Prefab이 성공적으로 수정되었습니다.");
    }

    public void MinusMaterial()
    {
        currentMaterial--;

        if (currentMaterial < 0)
            currentMaterial = Skin.Count - 1;


        skilPath = AssetDatabase.GetAssetPath(Skin[currentMaterial]);
        
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
        if (prefab == null)
        {
            print("프리팹없음");
            return;
        }
                
        GameObject instance = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
                
        Transform child = instance.transform.Find("ModelChanger");
        if (child == null)
        {
            print("자식없음");
            return;
        }

        GameObject prafabs = AssetDatabase.LoadAssetAtPath<GameObject>(skilPath);

        if (prafabs == null)
        {
            print(" 프프리팹없음");
            return;
        }
        
        GameObject instances = PrefabUtility.InstantiatePrefab(prafabs) as GameObject;
        
        instances.transform.SetParent(instance.transform);
        
        PrefabUtility.SaveAsPrefabAsset(instance, path);
        PrefabUtility.SaveAsPrefabAsset(instances, path);
        GameObject.DestroyImmediate(instance);
        GameObject.DestroyImmediate(instances);
    }
}
 