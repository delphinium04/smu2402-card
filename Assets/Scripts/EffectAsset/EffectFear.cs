using System.Collections;
using Effect;
using UnityEngine;

namespace EffectAsset
{
    [CreateAssetMenu(fileName = "Fear", menuName = "Effect/Fear", order = 1)]
    public class EffectFear : BaseEffect
    {
        protected override void Apply()
        {
            if (Target.TryGetComponent(out PlayerController player))
            {
                // Effect Add
            }
            else if (Target.TryGetComponent(out BaseEnemy enemy))
            {
                // Effect Add
            }
            else Debug.LogError($"{GetType().Name}: No BaseEnemy or PlayerController in {Target.name}");
        }

        public sealed override void Remove()
        {
            if (TurnIgnored > 0) return; // 이미 Remove() 되어있는 상황
            
            if (Target.TryGetComponent(out PlayerController player))
            {
                // Effect Remove
            }
            else if (Target.TryGetComponent(out BaseEnemy enemy))
            {
                // Effect Remove
            }
            else Debug.LogError($"{GetType().Name}: No BaseEnemy or PlayerController in {Target.name}");
        }
    }
}
