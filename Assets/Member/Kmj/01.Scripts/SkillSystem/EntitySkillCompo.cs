using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EntitySkillCompo : MonoBehaviour, IEntityComponent
{
    private Entity _entity;

    [FormerlySerializedAs("skillList")] [field: SerializeField] public UseSkillDataSO skillDataList;

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
        foreach (var skill in skillDataList.invenSkillDictionary)
        {
            if (skill.Key.currentcoolTime >= skill.Key.skillCoolTime)
                return;
            else
            {
                skill.Key.currentcoolTime += 1 * Time.deltaTime;
            }
        }


    }


    public bool CanUseSkill(SkillSO skillSO)
    {
        //��ų�� ���������� ������ ��ų�� CurrentCoolTime �� SkillCoolTime���� ũ�ų� ������
        //true�� ��ȯ �ϴϸ� false�� ��ȯ
        return skillSO.currentcoolTime >= skillSO.skillCoolTime ? true : false;
    }

    public void CurrentTimeClear(SkillSO skillSO)
    {
        //��ų�� Ŭ������
        skillSO.currentcoolTime = 0;
    }



}
