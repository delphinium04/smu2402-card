using Item;
using UnityEngine;

namespace ItemAsset
{
    // 동료 항해사
    [CreateAssetMenu(fileName = "BraveCaptain", menuName = "Item/Company/BraveCaptain")]
    public class CompanyBraveCaptain : BaseItem
    {
        protected override void Enable()
        {
            // Player.IgnoreEnvironmentTurns += 3;
        }

        public override void Disable()
        {
            return;
        }
    }
}
