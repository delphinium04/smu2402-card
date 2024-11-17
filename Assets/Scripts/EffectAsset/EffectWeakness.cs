using System.Collections;
using Effect;
using UnityEngine;

namespace EffectAsset
{
    [CreateAssetMenu(fileName = "Weakness", menuName = "Effect/Weakness", order = 1)]
    // 상태이상 약화
    public class EffectWeakness : BaseEffect
    {
        protected override void Apply()
        {
            if (Target.TryGetComponent(out PlayerController player))
            {
                player.AttackMultiplier -= 25;
                // player.damageMultiple -= 25 (%p); 100이 기본이라 가정
            }
            else if (Target.TryGetComponent(out EnemyBehaviour enemy))
            {
                enemy.AttackMultiplier -= 25;
            }
            else Debug.LogError($"{GetType().Name}: No EnemyBehaviour or PlayerController in {Target.name}");
        }

        public sealed override void Remove()
        {
            if (Target.TryGetComponent(out PlayerController player))
            {
                player.AttackMultiplier += 25;
                // player.damageMultiple -= 25 (%p); 100이 기본이라 가정
            }
            else if (Target.TryGetComponent(out EnemyBehaviour enemy))
            {
                enemy.AttackMultiplier += 25;
            }
            else Debug.LogError($"{GetType().Name}: No EnemyBehaviour or PlayerController in {Target.name}");
        }
    }
}
