using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public enum CurrencyType
{
    Eon, Test1
}

public enum ModifyType
{
    Set,
    Add,
    Substract,
    Multiply,
    Divine
}

public class CurrencyManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI testTxt;
    private Dictionary<CurrencyType, int> _currencyDic;
    
    public static CurrencyManager Instance;

    private void Awake()
    {
        _currencyDic = new Dictionary<CurrencyType, int>
        {
            { CurrencyType.Eon, 0 },
            { CurrencyType.Test1, 0 }
        };

        if (Instance == null)
        {
            Instance = this;        
            DontDestroyOnLoad(this);
        }
        else
            Destroy(gameObject);
        
        CurrencyManager.Instance.ModifyCurrency(CurrencyType.Eon, ModifyType.Set, 10000);
    }

    private void Update()
    {
        testTxt.text = _currencyDic[CurrencyType.Eon].ToString();
    }

    public int GetCurrency(CurrencyType currencyType) => _currencyDic[currencyType];

    public void ModifyCurrency(CurrencyType currencyType, ModifyType modifyType, int amount)
    {
        switch (modifyType)
        {
            case ModifyType.Set:
                _currencyDic[currencyType] = amount;
                break;
            case ModifyType.Add:
                _currencyDic[currencyType] += amount;
                break;
            case ModifyType.Substract:
                _currencyDic[currencyType] -= amount;
                break;
            case ModifyType.Multiply:
                _currencyDic[currencyType] *= amount;
                break;
            case ModifyType.Divine:
                _currencyDic[currencyType] /= amount;
                break;
        }
    }
}
