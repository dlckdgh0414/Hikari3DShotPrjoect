using UnityEngine;

namespace Member.Ysc._01_Code.Combats
{
    public interface IDamageable
    {
        public void ApplyDamage(float damage, Vector2 direction);
    }
}