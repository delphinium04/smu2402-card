using System.Collections;
using Effect;
using UnityEngine;  

namespace EffectAsset
{
    [CreateAssetMenu(fileName = "EffectWeakness", menuName = "Effect/Weakness", order = 1)]
    public class EffectWeakness : BaseEffect
    {
        // Called only once
        public override void Init(GameObject target, int duration)
        {
            if(Target != null) {
                Debug.LogError("EffectWeakness Init: Target already set!");
                return;
            }
            
            Target = target;
            TurnDuration = duration;
            TurnIgnored = 0;
            
            Apply();
            // target.EffectManager.AddEffect(this);
        }

        protected override void Apply()
        {
            Debug.Log($"{Target.name} {Type.Weakness.ToString()} 적용: 받는 데미지 증가");
            // target.defencedMultiple += _amount;
        }

        public sealed override void Remove()
        {
            Debug.Log($"{Target.name} {Type.Weakness.ToString()} 효과 제거됨");
            // target.defencedMultiple -= _amount;
        }

        public override void OnTurnPassed()
        {
            TurnDuration--;
            if (TurnDuration == 0)
            {
                // target.EffectManager.RemoveEffect(e)
            }

            if (TurnIgnored > 0)
            {
                TurnIgnored--;
                if (TurnDuration == 0)
                {
                    Debug.Log($"{effectName} reactivated");
                    Apply();
                }
            }
        }

        public override void IgnoreTurn(int turn)
        {
            TurnIgnored = turn;
            Remove();
            Debug.Log($"{effectName} deactivated");
        }

        public override void AddTurn(int turn)
        {
            TurnDuration += turn;
        }
    }
}
