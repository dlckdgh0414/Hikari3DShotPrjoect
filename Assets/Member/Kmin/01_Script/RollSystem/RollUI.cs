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
            eventChannel.AddListener<RollEndEvent>(HandleRollEnd);
            eventChannel.AddListener<RollEndEvent>(HandleRollEnd);
        }

        private void HandleRollEnd(RollEndEvent channel)
        {
            rollingText.text = channel.rolledSkill.name;
        }
    }
}
