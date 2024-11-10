using Effect;
using UnityEngine;  

namespace EffectAsset
{
    [CreateAssetMenu(fileName = "EffectWeakness", menuName = "Effect/Weakness", order = 1)]
    public class EffectWeakness : BaseEffect
    {
        public override void Init(GameObject target, int duration)
        {
            Target = target;
            TurnDuration = duration;
            Apply();
        }

        protected sealed override void Apply()
        {
            Debug.Log("ASDf");
            return;
            Debug.Log($"{Target.name} {Type.Weakness.ToString()} 적용: 받는 데미지 증가");
            // target.defencedMultiple += _amount;
            // target.EffectManager.AddEffect(this);
        }

        public sealed override void Remove()
        {
            Debug.Log($"{Target.name} {Type.Weakness.ToString()} 제거");
            // target.defencedMultiple += _amount;
            // target.EffectManager.AddEffect(this);
        }

        public override void IncreaseTurn()
        {
            TurnDuration--;
            if (TurnDuration == 0)
            {
                // target.EffectManager.RemoveEffect(e)
            }
        }

    }
}
