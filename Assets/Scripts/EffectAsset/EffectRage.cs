using System.Collections;
using Effect;
using Entity;
using UnityEngine;

namespace EffectAsset
{
    [CreateAssetMenu(fileName = "Rage", menuName = "Effect/Rage", order = 1)]
    public class EffectRage : BaseEffect
    {
        protected override void Apply()
        {
            if (GetComponent(Target.gameObject, out AbstractEntity entity))
                entity.AtkMultiplier += 25;
        }

        public sealed override void Remove()
        {
            if (TurnIgnored > 0) return; // 이미 Remove() 되어있는 상황
            
            if (GetComponent(Target.gameObject, out AbstractEntity entity))
                entity.AtkMultiplier -= 25;
        }
    }
}
