using System;
using System.Collections.Generic;
using System.IO;
using Member.Kmin._01_Script.Core.Save;
using UnityEngine;

public enum CurrencyType
{
    Eon
}

public enum ModifyType
{
    Set,
    Add,
    Multiply,
    Divine
}

[Serializable]
public class CurrencyManager : MonoBehaviour
{
    public Dictionary<CurrencyType, int> currencyDic;
    
    public static CurrencyManager Instance;

    public delegate void ValueChangedHanlder(CurrencyType type, int value);
    
    public event ValueChangedHanlder OnValueChanged;

    private void Awake()
    {
        currencyDic = new Dictionary<CurrencyType, int>
        {
            { CurrencyType.Eon, 0 },
        };

        if (Instance == null)
        {
            Instance = this;        
            DontDestroyOnLoad(this);
        }
        else
            Destroy(gameObject);
        
        ModifyCurrency(CurrencyType.Eon, ModifyType.Add, 1000);
    }

    public int GetCurrency(CurrencyType currencyType) => currencyDic[currencyType];

    public void ModifyCurrency(CurrencyType currencyType, ModifyType modifyType, int amount)
    {
        switch (modifyType)
        {
            case ModifyType.Set:
                currencyDic[currencyType] = amount;
                break;
            case ModifyType.Add:
                currencyDic[currencyType] += amount;
                break;
            case ModifyType.Multiply:
                currencyDic[currencyType] *= amount;
                break;
            case ModifyType.Divine:
                currencyDic[currencyType] /= amount;
                break;
        }
        
        OnValueChanged?.Invoke(currencyType, currencyDic[currencyType]);
    }

    public void SaveData()
    {
        string jsonData = DictionaryJsonUtility.ToJson(currencyDic, true);

        string path = Application.dataPath + "/Data";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        File.WriteAllText(path + "/CurrencyData.txt", jsonData);
    }

    public void LoadData()
    {
        string path = Application.dataPath + "/Data";
        string fromJsonData = File.ReadAllText(path + "/MonsterData.txt");

        CurrencyManager MonsterFromJson = new CurrencyManager();
        MonsterFromJson.monsters = DictionaryJsonUtility.FromJson<string, Monster>(fromJsonData);
        print(MonsterFromJson.monsters);
    }
}
