using UnityEngine;

namespace Member.Ysc._01_Code.Agent
{
    [CreateAssetMenu(fileName = "EntityFinder", menuName = "SO/Entity/Finder", order = 0)]
    public class TestPlayerFinderSO : ScriptableObject
    {
        [SerializeField] private string targetTag;
        public TestPlayer target;

        public void SetPlayer(TestPlayer player)
        {
            target = player;
        }
    }
}