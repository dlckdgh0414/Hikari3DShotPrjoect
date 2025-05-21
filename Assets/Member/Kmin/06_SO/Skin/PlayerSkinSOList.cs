using System.Collections.Generic;
using UnityEngine;

namespace Member.Kmin._06_SO.Skin
{
    [CreateAssetMenu(fileName = "PlayerSkinSOList", menuName = "SO/PlayerSkinSOList")]
    public class PlayerSkinSOList : ScriptableObject
    {
        public List<PlayerSkinSO> skinList;
    }
}
