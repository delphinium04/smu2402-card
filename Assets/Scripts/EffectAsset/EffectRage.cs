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
                player.AttackMultiplier += 25;
            }
            else if (Target.TryGetComponent(out EnemyBehaviour enemy))
            {
                enemy.AttackMultiplier += 25;
            }
            else Debug.LogError($"{GetType().Name}: No EnemyBehaviour or PlayerController in {Target.name}");
        }

        public sealed override void Remove()
        {
            if (TurnIgnored > 0) return; // 이미 Remove() 되어있는 상황
            
            if (Target.TryGetComponent(out PlayerController player))
            {               
                player.AttackMultiplier -= 25;
            }
            else if (Target.TryGetComponent(out EnemyBehaviour enemy))
            {
                enemy.AttackMultiplier -= 25;
            }
            else Debug.LogError($"{GetType().Name}: No EnemyBehaviour or PlayerController in {Target.name}");
        }
    }
}
