using System.Collections.Generic;
using UnityEngine;

public class EntitySkillCompo : MonoBehaviour, IEntityComponent
{
    private Entity _entity;

    public List<SkillSO> skills;

    [field: SerializeField] public SkillInventorySO skillList;

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
        foreach (var skill in skillList.TotalSkillList)
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
        skills.ForEach(skill => skillList.TotalSkillList.Add(skill.skillName, skill));
    }

    public bool CanUseSkill(string name)
    {
        //��ų�� ���������� ������ ��ų�� CurrentCoolTime �� SkillCoolTime���� ũ�ų� ������
        //true�� ��ȯ �ϴϸ� false�� ��ȯ
        if (skillList.TotalSkillList.GetValueOrDefault(name).currentcoolTime >=
           skillList.TotalSkillList.GetValueOrDefault(name).skillCoolTime)
            return true;
        else
            return false;

    }

    public void CurrentTimeClear(string name)
    {
        //��ų�� Ŭ������
        skillList.TotalSkillList.GetValueOrDefault(name).currentcoolTime = 0;
    }



}
