using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum CurrencyType
{
    Eon
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
    private Dictionary<CurrencyType, int> _currencyDic;
    
    public static CurrencyManager Instance;

    private void Awake()
    {
        _currencyDic = new Dictionary<CurrencyType, int>();
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
