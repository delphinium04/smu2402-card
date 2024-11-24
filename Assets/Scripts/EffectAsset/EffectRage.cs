using System.Collections;
using Effect;
using Enemy;
using UnityEngine;

namespace EffectAsset
{
    [CreateAssetMenu(fileName = "Rage", menuName = "Effect/Rage", order = 1)]
    public class EffectRage : BaseEffect
    {
        protected override void Apply()
        {
            Debug.Log(Target.gameObject);
            if (Target.TryGetComponent(out BaseEnemy entity))
                entity.AtkMultiplier += 25;
            else
                PlayerController.Instance.AtkMultiplier += 25;
        }

        public sealed override void Remove()
        {
            if (TurnIgnored > 0) return; // 이미 Remove() 되어있는 상황

            if (Target.TryGetComponent(out BaseEnemy entity))
                entity.AtkMultiplier -= 25;
            else
                PlayerController.Instance.AtkMultiplier -= 25;
        }

        public override BaseEffect Clone()
            => ScriptableObject.CreateInstance<EffectRage>();
    }
}