using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Skill/So", fileName = "SKills")]

public class SkillSO : ScriptableObject
{
    public string skillName;
    public Sprite skillUIImage;
    public float skillDamage;
    public float skillCoolTime;
    public float currentcoolTime;

    private void OnValidate()
    {
        //CurrentCoolTime이 SkillCoolTime보다 크면 SkillCoolTime으로 초기화
        if (currentcoolTime >= skillCoolTime)
            currentcoolTime = skillCoolTime;
    }
}
