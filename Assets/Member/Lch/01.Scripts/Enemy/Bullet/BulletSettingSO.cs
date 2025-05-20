using UnityEngine;

[CreateAssetMenu(fileName = "BulletScripts", menuName = "SO/BulletSetting")]
public class BulletSettingSO : ScriptableObject
{
    public string BulletName;
    public int BulletCount;
    public float BulletSpeed;
    public float BulletDamage;

    [TextArea]
    public string BulletDescript;
}
