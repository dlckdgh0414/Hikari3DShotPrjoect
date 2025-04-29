using UnityEngine;

namespace Member.Kmin._01_Script.RollSystem
{
    public class AfterRoll : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO eventChannel;

        private void Awake()
        {
            eventChannel.AddListener<RollEndEvent>(HandleRollEnd);
        }

        private void HandleRollEnd(RollEndEvent rollEvent)
        {
            //if(rollEvent.rolledSkill)
        }
    }
}
