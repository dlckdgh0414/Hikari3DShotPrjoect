using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Member.Kmin._01_Script.RollSystem
{
    public class RollUI : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO eventChannel;
        [SerializeField] private TextMeshProUGUI rollingText;
        [SerializeField] private TextMeshProUGUI rarityText;

        private void Awake()
        {
            eventChannel.AddListener<RollEvent>(HandleOnRolling);
            eventChannel.AddListener<RollEndEvent>(HandleRollEnd);
        }

        private void HandleRollEnd(RollEndEvent channel)
        {
            RollDataSO skill = channel.rolledSkill;

            rarityText.gameObject.SetActive(true);
            rarityText.text = $"1 in {skill.rarity}";

        }

        private void HandleOnRolling(RollEvent channel)
        {
            if (rarityText.IsActive()) rarityText.gameObject.SetActive(false);

            RollDataSO skill = channel.rolledSkill;

            rollingText.text = skill.skillText.text;
            rollingText.color = skill.skillText.color;
            rollingText.font = skill.skillText.font;

            rollingText.rectTransform.position = new Vector3 (rollingText.transform.position.x, 470, 0);

            rollingText.rectTransform.DOLocalMoveY(0f, 0.2f).SetEase(Ease.OutSine);
        }
    }
}
