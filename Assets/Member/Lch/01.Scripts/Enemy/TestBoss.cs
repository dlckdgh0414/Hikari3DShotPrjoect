using Member.Ysc._01_Code.UI;
using UnityEngine;

public class TestBoss : BTBoss
{
    public override void InitObject()
    {
        //OnDead.AddListener(FindAnyObjectByType<GameProgressCheckUI>().CheatClear);
        //boss.OnDead.AddListener(CheatClear);
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        OnDead.AddListener(FindAnyObjectByType<GameProgressCheckUI>().CheatClear);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        OnDead.RemoveListener(FindAnyObjectByType<GameProgressCheckUI>().CheatClear);
    }
}
