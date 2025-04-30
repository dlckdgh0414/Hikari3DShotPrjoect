using System.Collections.Generic;
using UnityEngine;

public class EntitySkillCompo : MonoBehaviour, IEntityComponent
{
    private Entity _entity;

    [field: SerializeField] public UseSkillSO skillList;

    public void Initialize(Entity entity)
    {
        _entity = entity;
    }

    private void Awake()
    {
    }


    private void Update()
    {
        //foreach�� ������ ��ų ����Ʈ�� �ִ� SO�߿� currentCoolTime�� SkillCoolTime���� ������
        //1�� �� �����ش�.
        foreach (var skill in skillList.UseSkillDictionary)
        {
            if (skill.Value.currentcoolTime >= skill.Value.skillCoolTime)
                return;
            else
            {
                skill.Value.currentcoolTime += 1 * Time.deltaTime;
            }
        }


    }


    public bool CanUseSkill(string name)
    {
        //��ų�� ���������� ������ ��ų�� CurrentCoolTime �� SkillCoolTime���� ũ�ų� ������
        //true�� ��ȯ �ϴϸ� false�� ��ȯ
        if (skillList.UseSkillDictionary.GetValueOrDefault(name).currentcoolTime >=
           skillList.UseSkillDictionary.GetValueOrDefault(name).skillCoolTime)
            return true;
        else
            return false;

    }

    public void CurrentTimeClear(string name)
    {
        //��ų�� Ŭ������
        skillList.UseSkillDictionary.GetValueOrDefault(name).currentcoolTime = 0;
    }



}
