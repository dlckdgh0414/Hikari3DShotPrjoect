using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class SkillCoolDownUI : MonoBehaviour,IEntityComponent
{
    protected Skill _currentSkill;
    private float _cooltime;

    protected Image _iconCool;
    protected Image _iconImage;
    protected SkillCompo _skillCompo;

    public TextMeshProUGUI text;

    private bool isEndCool;

    [field:SerializeField]
    public InputReader InputReader { get; private set; }

    public void Initialize(Entity entity)
    {
        _iconImage = GetComponent<Image>();
        _iconCool = transform.GetChild(0).GetComponent<Image>();
        text.gameObject.SetActive(false);
        _skillCompo = entity.GetCompo<SkillCompo>();
        InitializeCooldownUI();
        InputAction firSkillInput = InputReader._controlls.FindAction("FirSkill");
        InputAction secSkillInput = InputReader._controlls.FindAction("SecSkill");
        InputAction thrSkillInput = InputReader._controlls.FindAction("ThrSkill");

        foreach(var d in firSkillInput.bindings)
        {
            Debug.Log(d);
        }
    }

    protected virtual void InitializeCooldownUI()
    {
        text.gameObject.SetActive(false);
    }

    protected virtual void CooldownInfo(float current, float totalTime)
    {
        Debug.Log($"{current} / {totalTime}");
        TextSet(current,totalTime);
        _cooltime = totalTime;
        _iconCool.fillAmount = current / _cooltime;
    }
    private void TextSet(float current, float totalTime)
    {
        bool isAtv = current < 0.1f ? false : true;
        text.gameObject.SetActive(isAtv);

        if(current <= 10)
        text.text = (current + 1).ToString().Substring(0, 1);
        else
            text.text = (current + 1).ToString().Substring(0, 2);
    }
}
