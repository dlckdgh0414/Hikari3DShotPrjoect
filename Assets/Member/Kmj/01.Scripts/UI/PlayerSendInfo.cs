using System.Collections.Generic;
using UnityEngine;

public class PlayerSendInfo : MonoSingleton<PlayerSendInfo>
{
    public PlayerSkinSO ThisSkill;
    public List<string> skillName=new();

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);

        if (gameObject == null)
        {
            GameObject.FindAnyObjectByType<SettingManager>();
            if (gameObject == null)
            {
                Instantiate(gameObject);
            }
        }

        else
            return;
    }
    public bool DontSelectAllSkills()
    {
        foreach(string s in skillName)
        {
            if (s.Length < 1)
                return true;
        }
        return false;
    }
}
