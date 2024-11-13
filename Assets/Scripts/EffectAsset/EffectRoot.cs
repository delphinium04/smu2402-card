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
            // 일반카드 선택 제한 설정 (오직 플레이어 대상)
        }

        public sealed override void Remove()
        {
            // 일반카드 선택 제한 해제 (오직 플레이어 대상)
        }
    }
}
