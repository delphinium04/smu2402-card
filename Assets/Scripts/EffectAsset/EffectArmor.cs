using System.Collections;
using Effect;
using UnityEngine;

namespace EffectAsset
{
    [CreateAssetMenu(fileName = "Armor", menuName = "Effect/Armor", order = 1)]
    public class EffectArmor : BaseEffect
    {
        protected override void Apply()
        {
            if (Target.TryGetComponent(out PlayerController player))
            {
                // player.defenceMultiple += 50 (%p); 100이 기본, 증가할 수록 좋다고 가정
            }
            else if (Target.TryGetComponent(out BaseEnemy enemy))
            {
                // enemy.defenceMultiple += 50 (%p);
            }
            else Debug.LogError($"{GetType().Name}: No BaseEnemy or PlayerController in {Target.name}");
        }

        public sealed override void Remove()
        {
            if (TurnIgnored > 0) return; // 이미 Remove() 되어있는 상황
            
            if (Target.TryGetComponent(out PlayerController player))
            {
                // player.defenceMultiple -= 50 (%p); 100이 기본, 증가할 수록 좋다고 가정
            }
            else if (Target.TryGetComponent(out BaseEnemy enemy))
            {
                // enemy.defenceMultiple -= 50 (%p);
            }
            else Debug.LogError($"{GetType().Name}: No BaseEnemy or PlayerController in {Target.name}");
        }
    }
}
