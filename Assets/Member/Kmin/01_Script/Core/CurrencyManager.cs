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
            try
            {
                LoadCurrencyData();
            }
            catch (Exception e)
            {
                Debug.LogWarning("Failed to load currency data, resetting. " + e);
                SaveCurrencyData();
            }
        }

        if (GetCurrency(CurrencyType.Eon) == 0)
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
        currencyDic.Clear();

        foreach (CurrencyType type in Enum.GetValues(typeof(CurrencyType)))
        {
            currencyDic[type] = 0;
        }

        CurrencyData data = SaveLoadManager.Load<CurrencyData>();
        foreach (var entry in data.currencyList)
        {
            currencyDic[entry.type] = entry.amount;
        }

        foreach (var pair in currencyDic)
        {
            OnValueChanged?.Invoke(pair.Key, pair.Value);
        }
    }
}
