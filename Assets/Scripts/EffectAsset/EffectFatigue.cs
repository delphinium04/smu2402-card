using System.Collections;
using Effect;
using Enemy;
using UnityEngine;

namespace EffectAsset
{
    [CreateAssetMenu(fileName = "Fatigue", menuName = "Effect/Fatigue", order = 1)]
    public class EffectFatigue : BaseEffect
    {
        protected override void Apply()
        {
            if (Target.TryGetComponent(out BaseEnemy entity))
                entity.TakeMultiplier += 50;
            else
                PlayerController.Instance.TakeMultiplier += 50;
        }

        public sealed override void Remove()
        {
            if (TurnIgnored > 0) return; // 이미 Remove() 되어있는 상황

            if (Target.TryGetComponent(out BaseEnemy entity))
                entity.TakeMultiplier -= 50;
            else
                PlayerController.Instance.TakeMultiplier -= 50;
        }

        public override BaseEffect Clone()
            => ScriptableObject.CreateInstance<EffectFatigue>();
    }
}