using System.Collections;
using Effect;
using UnityEngine;

namespace EffectAsset
{
    [CreateAssetMenu(fileName = "Root", menuName = "Effect/Root", order = 1)]
    // 상태이상 약화
    public class EffectRoot : BaseEffect
    {
        protected override void Apply()
        {
            PlayerController.Instance.canSelectNormalCard = false;
        }

        public sealed override void Remove()
        {
            PlayerController.Instance.canSelectNormalCard = true;
        }
    }
}
