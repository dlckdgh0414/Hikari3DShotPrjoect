using System.IO;
using UnityEditor;
using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "RollSO", menuName = "SO/RollSO")]
public class RollDataSO : ScriptableObject
{
    public string name;
    public int rarity;
    [TextArea]
    public string description = string.Empty;
    public TextMeshProUGUI skillText;

    private string prefabPath = "Assets/Member/KimMin/03Prefab/SkillText";

#if UNITY_EDITOR
    /*private void OnValidate()
    {
        string path = AssetDatabase.GetAssetPath(this);
        string fileName = Path.GetFileNameWithoutExtension(path);

        string[] files = Directory.GetFiles(prefabPath, "*.prefab", SearchOption.AllDirectories);

        foreach (string file in files)
        {
            string prefabFileName = Path.GetFileNameWithoutExtension(file);

            if (fileName == prefabFileName)
            {
                skillText = AssetDatabase.LoadAssetAtPath<TextMeshProUGUI>(file);
            }
        }
    }*/
#endif
}
