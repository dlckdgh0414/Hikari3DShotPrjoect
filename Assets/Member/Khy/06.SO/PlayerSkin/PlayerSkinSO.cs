using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkinSO", menuName = "SO/PlayerSkinSO")]
public class PlayerSkinSO : ScriptableObject
{
    public GameObject playerPrefab;
    public float speed;
    public float defalutAttackSpeed;
    public float hp;
}
