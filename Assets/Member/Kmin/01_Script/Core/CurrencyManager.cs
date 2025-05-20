using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using VHierarchy.Libs;

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
public class CurrencyEntry
{
    public CurrencyType type;
    public int amount;
}

[Serializable]
public class CurrencyData
{
    public List<CurrencyEntry> currencyList = new();
}

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
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        SaveLoadManager.SetFilePath(Application.persistentDataPath, "currency.json");

        if (SaveLoadManager.CheckFile())
        {
            LoadCurrencyData();
        }
        else
        {
            ModifyCurrency(CurrencyType.Eon, ModifyType.Add, 1000);
            SaveCurrencyData();
        }
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
                if (amount != 0)
                    currencyDic[currencyType] /= amount;
                break;
        }

        OnValueChanged?.Invoke(currencyType, currencyDic[currencyType]);
        SaveCurrencyData();
    }

    public void SaveCurrencyData()
    {
        CurrencyData data = new CurrencyData();

        foreach (var pair in currencyDic)
        {
            data.currencyList.Add(new CurrencyEntry
            {
                type = pair.Key,
                amount = pair.Value
            });
        }

        SaveLoadManager.Save(data);
    }

    public void LoadCurrencyData()
    {
        CurrencyData data = SaveLoadManager.Load<CurrencyData>();

        currencyDic.Clear();
        
        data.currencyList.ForEach(data => currencyDic[data.type] = data.amount);
        currencyDic.ForEach(data => OnValueChanged?.Invoke(data.Key, data.Value));
    }
}
