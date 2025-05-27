using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static string SavePath => Application.persistentDataPath + "/saves/";

    public static void Save(SaveData saveData, int saveFileName)
    {
        if (!Directory.Exists(SavePath))
        {
            Directory.CreateDirectory(SavePath);
        }

        string saveJson = JsonUtility.ToJson(saveData);
        string saveFilePath = SavePath + saveFileName + ".json";
        File.WriteAllText(saveFilePath, saveJson);
        Debug.Log("�����: " + saveFilePath);
        
    }

    public static SaveData Load(int saveFileName)
    {
        string saveFilePath = SavePath + saveFileName + ".json";
        Debug.Log(saveFilePath);
        if (!File.Exists(saveFilePath))
        {
            Debug.LogError("�̰� ���� ��ã�� z");
            return null;
        }

        string saveFile = File.ReadAllText(saveFilePath);
        SaveData saveData = JsonUtility.FromJson<SaveData>(saveFile);
        return saveData;
    }

    public static void SaveDeleteAll()
    {
        if (Directory.Exists(SavePath))
        {
            Directory.Delete(SavePath, true);
            Debug.Log("��� ���� ������ ������");
        }
        else
        {
            Debug.LogWarning("������ ���� ������ ����");
        }
    }
}
