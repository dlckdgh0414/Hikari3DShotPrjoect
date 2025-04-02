using UnityEngine;

namespace Member.Ysc._01_Code.Agent
{
    public class EntityFeedbackData : MonoBehaviour, IEntityComponent
    {
        [field: SerializeField] public bool IsLastHitCritical { get; set; }
        [field: SerializeField] public Vector2 LastAttackDirection {get; set;}
        [field: SerializeField] public Entity LastEntityWhoHit { get; set; }

        private Entity _entity;
        public void Initialize(Entity entity)
        {
            _entity = entity;
        }
    }
}