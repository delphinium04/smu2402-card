using System.Collections;
using Effect;
using UnityEngine;

namespace EffectAsset
{
    [CreateAssetMenu(fileName = "Silence", menuName = "Effect/Silence", order = 1)]
    // 상태이상 약화
    public class EffectSilence : BaseEffect
    {
        protected override void Apply()
        {
            if (Target.TryGetComponent(out PlayerController player))
            {
                // player.damageMultiple -= 25 (%p); 100이 기본이라 가정
            }
            else if (Target.TryGetComponent(out BaseEnemy enemy))
            {
                // enemy.damageMultiple -= 25 (%p); 100이 기본이라 가정
            }
            else Debug.LogError($"{GetType().Name}: No BaseEnemy or PlayerController in {Target.name}");
        }

        public sealed override void Remove()
        {
            // player. -= 50;
        }
    }
}
