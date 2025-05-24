using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class SkillCoolDownUI : MonoBehaviour,IEntityComponent
{
    protected ActiveSkill _currentSkill;
    private float _cooltime;

    protected Image _iconCool;
    protected Image _iconImage;
    protected SkillCompo _skillCompo;

    public TextMeshProUGUI text;
    public TextMeshProUGUI whatKeyText;

    private bool isEndCool;

    [field:SerializeField]
    public InputReader InputReader { get; private set; }
    [field:SerializeField]
    protected InputActionReference _skillInputKey;

    public void Initialize(Entity entity)
    {
        _iconImage = GetComponent<Image>();
        _iconCool = transform.GetChild(0).GetComponent<Image>();
        text.gameObject.SetActive(false);
        _skillCompo = entity.GetCompo<SkillCompo>();
        InitializeCooldownUI();
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
