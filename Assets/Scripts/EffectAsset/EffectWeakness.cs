using System.Collections;
using Effect;
using Enemy;
using UnityEngine;

namespace EffectAsset
{
    [CreateAssetMenu(fileName = "Weakness", menuName = "Effect/Weakness", order = 1)]
    // 상태이상 약화
    public class EffectWeakness : BaseEffect
    {
        protected override void Apply()
        {
            if (Target.TryGetComponent(out BaseEnemy entity))
                entity.AtkMultiplier -= 25;
            else PlayerController.Instance.AtkMultiplier -= 25;
        }

        public sealed override void Remove()
        {
            if (Target.TryGetComponent(out BaseEnemy entity))
                entity.AtkMultiplier += 25;
            else
                PlayerController.Instance.AtkMultiplier -= 25;
        }

        public override BaseEffect Clone()
            => ScriptableObject.CreateInstance<EffectWeakness>();
    }
}