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
            PlayerController.Instance.canSelectSkillCard = false;
        }

        public sealed override void Remove()
        {
            PlayerController.Instance.canSelectSkillCard = true;
        }
    }
}