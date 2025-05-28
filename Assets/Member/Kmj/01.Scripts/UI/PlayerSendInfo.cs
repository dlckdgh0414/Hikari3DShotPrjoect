using System.Collections.Generic;
using UnityEngine;

public class PlayerSendInfo : MonoSingleton<PlayerSendInfo>
{
    public PlayerSkinSO ThisSkill;
    public string[] skillName = { "","",""};

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);

        if (Instance == null)
        {
            Instantiate(Instance);
        }
        else
            return;
    }
    public void ResetSkills()
    {
        for(int i =0;i<skillName.Length;i++)
        {
            skillName[i] = "";
        }
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
