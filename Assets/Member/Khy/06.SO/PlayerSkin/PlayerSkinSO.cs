using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "PlayerSkinSO", menuName = "SO/PlayerSkinSO")]
public class PlayerSkinSO : ScriptableObject
{
    public string name;
    public int rarity;
    public Sprite icon;
    [TextArea] public string description = string.Empty;
    public Color itemColor;
    public float speed;
}
