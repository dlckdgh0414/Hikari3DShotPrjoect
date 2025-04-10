using System.Collections.Generic;
using UnityEngine;

public class EntitySkillCompo : MonoBehaviour, IEntityComponent
{
    private Entity _entity;

    public List<SkillSO> skills;

    public Dictionary<string, SkillSO> skillList = new Dictionary<string, SkillSO>();
    public void Initialize(Entity entity)
    {
        _entity = entity;
    }

    private void Awake()
    {
        AddSkillInDictionary();
    }


    private void Update()
    {
        //foreach�� ������ ��ų ����Ʈ�� �ִ� SO�߿� currentCoolTime�� SkillCoolTime���� ������
        //1�� �� �����ش�.
        foreach (var skill in skillList)
        {
            if (skill.Value.currentcoolTime >= skill.Value.skillCoolTime)
                return;
            else
            {
                skill.Value.currentcoolTime += 1 * Time.deltaTime;
            }
        }


    }
    private void AddSkillInDictionary()
    {
        //����Ʈ�� �ִ� ��ų SO���� Dictionary�� �ִ� �ڵ�
        skills.ForEach(skill => skillList.Add(skill.skillName, skill));
    }

    public bool CanUseSkill(string name)
    {
        //��ų�� ���������� ������ ��ų�� CurrentCoolTime �� SkillCoolTime���� ũ�ų� ������
        //true�� ��ȯ �ϴϸ� false�� ��ȯ
        if (skillList.GetValueOrDefault(name).currentcoolTime >=
           skillList.GetValueOrDefault(name).skillCoolTime)
            return true;
        else
            return false;

    }

    public void CurrentTimeClear(string name)
    {
        //��ų�� Ŭ������
        skillList.GetValueOrDefault(name).currentcoolTime = 0;
    }



}
