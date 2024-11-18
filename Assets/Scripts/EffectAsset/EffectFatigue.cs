using System.Collections;
using Effect;
using Entity;
using UnityEngine;

namespace EffectAsset
{
    [CreateAssetMenu(fileName = "Fatigue", menuName = "Effect/Fatigue", order = 1)]
    public class EffectFatigue : BaseEffect
    {
        protected override void Apply()
        {
            if (GetComponent(Target.gameObject, out AbstractEntity entity))
                entity.TakeMultiplier += 50;
        }

        public sealed override void Remove()
        {
            if (TurnIgnored > 0) return; // 이미 Remove() 되어있는 상황
            
            if (Target.TryGetComponent(out AbstractEntity entity))
                entity.TakeMultiplier -= 50;
            else Debug.LogError($"{GetType().Name}: No AbstractEntity in {Target.name}");
        }
    }
}
