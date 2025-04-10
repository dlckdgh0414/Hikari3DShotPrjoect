using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

public class Roll : MonoBehaviour
{
    [SerializeField] private GameEventChannelSO rollEventChannel;
    [SerializeField] private SkillSOList skillList;

    [SerializeField] private float luck;

    private Dictionary<string, RollDataSO> _skillDic/*k*/ = new Dictionary<string, RollDataSO>();

    private RollDataSO _rolledSkill = null;

    private void Awake()
    {
        foreach(RollDataSO skill in skillList.skillList)
        {
            _skillDic.Add(skill.name, skill);
        }
    }

    private bool IsPicked(float rarity)
    {
        return Random.Range(1, (int)rarity) == 1;
    }

    public void SkillRoll()
    {
        StartCoroutine(RollRoutine());
    }

    private void Rolling()
    {
        RollEvent rollEvent = RollEventChannel.RollEvent;

        foreach (RollDataSO skill in _skillDic.Values.Reverse())
        {
            if (IsPicked(skill.rarity / luck))
            {
                _rolledSkill = skill;
                break;
            }
        }

        rollEvent.rolledSkill = _rolledSkill;
        rollEventChannel.RaiseEvent(rollEvent);
    }

    private IEnumerator RollRoutine()
    {
        RollEndEvent rollEnd = RollEventChannel.RollEndEvent;

        for(int i = 0; i < 10; i++)
        {
            Rolling();
            yield return new WaitForSeconds(0.2f);
        }

        RollEventChannel.RollEndEvent.rolledSkill = _rolledSkill;
        rollEventChannel.RaiseEvent(RollEventChannel.RollEndEvent);
    }
}
