using UnityEngine;

namespace Member.Ysc._01_Code.Agent
{
    [CreateAssetMenu(fileName = "EntityFinder", menuName = "SO/Entity/Finder", order = 0)]
    public class EntityFinderSO : ScriptableObject
    {
        [SerializeField] private string targetTag;
        public Entity target;

        public void SetPlayer(Entity entity)
        {
            target = entity;
        }
    }
}