using System;
using System.Collections.Generic;
using UnityEngine;

namespace Member.Kmin._01_Script.Core.Save
{
    [Serializable]
    public class DataDictionary<TKey, TValue>
    {
        public TKey Key;
        public TValue Value;
    }

    [Serializable]
    public class JsonDataArray<TKey, TValue>
    {
        public List<DataDictionary<TKey, TValue>> data;
    }

    public static class DictionaryJsonUtility
    {
        public static string ToJson<TKey, TValue>(Dictionary<TKey, TValue> jsonDicData, bool pretty = false)
        {
            List<DataDictionary<TKey, TValue>> dataList = new List<DataDictionary<TKey, TValue>>();
            DataDictionary<TKey, TValue> dictionaryData;
            foreach (TKey key in jsonDicData.Keys)
            {
                dictionaryData = new DataDictionary<TKey, TValue>();
                dictionaryData.Key = key;
                dictionaryData.Value = jsonDicData[key];
                dataList.Add(dictionaryData);
            }
            JsonDataArray<TKey, TValue> arrayJson = new JsonDataArray<TKey, TValue>();
            arrayJson.data = dataList;

            return JsonUtility.ToJson(arrayJson, pretty);
        }
    
        public static Dictionary<TKey, TValue> FromJson<TKey, TValue>(string jsonData)
        {
            List<DataDictionary<TKey, TValue>> dataList = JsonUtility.FromJson<List<DataDictionary<TKey, TValue>>>(jsonData);

            Dictionary<TKey, TValue> returnDictionary = new Dictionary<TKey, TValue>();

            for (int i = 0; i < dataList.Count; i++)
            {
                DataDictionary<TKey, TValue> dictionaryData = dataList[i];
                returnDictionary[dictionaryData.Key] = dictionaryData.Value;
            }

            return returnDictionary;
        }
    }
}