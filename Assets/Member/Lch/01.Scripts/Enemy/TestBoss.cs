using Ami.BroAudio;
using Member.Ysc._01_Code.UI;
using UnityEngine;

public class TestBoss : BTBoss
{
    [SerializeField]
    private SoundID bossSound;
    public override void InitObject()
    {
        //OnDead.AddListener(FindAnyObjectByType<GameProgressCheckUI>().CheatClear);
        //boss.OnDead.AddListener(CheatClear);
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        OnDead.AddListener(FindAnyObjectByType<GameProgressCheckUI>().CheatClear);
        BroAudio.Play(bossSound);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        OnDead.RemoveListener(FindAnyObjectByType<GameProgressCheckUI>().CheatClear);
    }
}
