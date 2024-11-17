using System.Collections;
using Effect;
using UnityEngine;

namespace EffectAsset
{
    [CreateAssetMenu(fileName = "Fatigue", menuName = "Effect/Fatigue", order = 1)]
    public class EffectFatigue : BaseEffect
    {
        protected override void Apply()
        {
            if (Target.TryGetComponent(out PlayerController player))
            {
                player.TakeMultiplier += 50;
            }
            else if (Target.TryGetComponent(out EnemyBehaviour enemy))
            {
                enemy.TakeMultiplier += 50;
            }
            else Debug.LogError($"{GetType().Name}: No EnemyBehaviour or PlayerController in {Target.name}");
        }

        public sealed override void Remove()
        {
            if (TurnIgnored > 0) return; // 이미 Remove() 되어있는 상황
            
            if (Target.TryGetComponent(out PlayerController player))
            {
                player.TakeMultiplier -= 50;
            }
            else if (Target.TryGetComponent(out EnemyBehaviour enemy))
            {               
                enemy.TakeMultiplier -= 50;
            }
            else Debug.LogError($"{GetType().Name}: No EnemyBehaviour or PlayerController in {Target.name}");
        }
    }
}
