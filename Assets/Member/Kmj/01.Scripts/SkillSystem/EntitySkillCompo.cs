using System.Collections.Generic;
using UnityEngine;

public class EntitySkillCompo : MonoBehaviour, IEntityComponent
{
    private Entity _entity;

    public List<SkillSO> skills;

    [field: SerializeField] public SelectActiveBtn skillList;

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
        //foreach로 돌려서 스킬 리스트에 있는 SO중에 currentCoolTime이 SkillCoolTime보다 작으면
        //1초 씩 더해준다.
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
    private void AddSkillInDictionary()
    {
        //리스트에 있는 스킬 SO들을 Dictionary에 넣는 코드
        skills.ForEach(skill => skillList.UseSkillDictionary.Add(skill.skillName, skill));
    }

    public bool CanUseSkill(string name)
    {
        //스킬을 실행했을떄 정해진 스킬이 CurrentCoolTime 이 SkillCoolTime보다 크거나 같으면
        //true를 반환 하니면 false를 반환
        if (skillList.UseSkillDictionary.GetValueOrDefault(name).currentcoolTime >=
           skillList.UseSkillDictionary.GetValueOrDefault(name).skillCoolTime)
            return true;
        else
            return false;

    }

    public void CurrentTimeClear(string name)
    {
        //스킬을 클리어함
        skillList.UseSkillDictionary.GetValueOrDefault(name).currentcoolTime = 0;
    }



}
