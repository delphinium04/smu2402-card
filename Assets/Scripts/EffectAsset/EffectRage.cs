using System.Collections;
using Effect;
using UnityEngine;

namespace EffectAsset
{
    [CreateAssetMenu(fileName = "Rage", menuName = "Effect/Rage", order = 1)]
    public class EffectRage : BaseEffect
    {
        protected override void Apply()
        {
            if (Target.TryGetComponent(out PlayerController player))
            {
                // player.attackMultiple += 25 (%p); 100이 기본, 증가할 수록 좋다고 가정
            }
            else if (Target.TryGetComponent(out BaseEnemy enemy))
            {
                // enemy.defenceMultiple -= 25 (%p);
            }
            else Debug.LogError($"{GetType().Name}: No BaseEnemy or PlayerController in {Target.name}");
        }

        public sealed override void Remove()
        {
            if (TurnIgnored > 0) return; // 이미 Remove() 되어있는 상황
            
            if (Target.TryGetComponent(out PlayerController player))
            {
                // player.defenceMultiple -= 25 (%p); 100이 기본, 증가할 수록 좋다고 가정
            }
            else if (Target.TryGetComponent(out BaseEnemy enemy))
            {
                // enemy.defenceMultiple -= 25 (%p); 100이 기본, 증가할 수록 좋다고 가정
            }
            else Debug.LogError($"{GetType().Name}: No BaseEnemy or PlayerController in {Target.name}");
        }
    }
}
