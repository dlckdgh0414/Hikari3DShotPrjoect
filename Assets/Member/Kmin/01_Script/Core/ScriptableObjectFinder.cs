using UnityEditor;
using UnityEngine;
public class ScriptableObjectFinder : MonoBehaviour
{
    public static T LoadScriptableObjectFromFolder<T>(string folderPath) where T : ScriptableObject
    {
        string[] guids = AssetDatabase.FindAssets($"t:{typeof(T).Name}", new[] { folderPath });
        string path = AssetDatabase.GUIDToAssetPath(guids[0]);
        return AssetDatabase.LoadAssetAtPath<T>(path);
    }
}
