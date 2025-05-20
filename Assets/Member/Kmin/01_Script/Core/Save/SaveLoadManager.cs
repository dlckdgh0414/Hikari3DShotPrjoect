using UnityEngine;
using System.IO;

public static class SaveLoadManager
{
    private static string saveFolderPath;
    private static string saveFilePath = saveFolderPath;
    
    public static void SetFilePath(string _path, string _fileName)
    {
        saveFolderPath = _path;
        saveFilePath = saveFolderPath + "/" + _fileName;
    }

    public static void Save<Tclass>(Tclass data)
    {
        CheckFile();

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(saveFilePath, json);
    }
    
    public static Tclass Load<Tclass>()
    {
        CheckFile();

        string json = File.ReadAllText(saveFilePath);
        Tclass target = JsonUtility.FromJson<Tclass>(json);

        return target;
    }
    
    public static bool CheckFile()
    {
        if (!Directory.Exists(saveFolderPath))
            Directory.CreateDirectory(saveFolderPath);

        if (!File.Exists(saveFilePath))
        {
            FileStream file = File.Create(saveFilePath);
            file.Close();

            return false;
        }

        return true;
    }
}