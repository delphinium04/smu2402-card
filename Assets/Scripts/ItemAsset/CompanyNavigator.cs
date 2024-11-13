using Item;
using UnityEngine;

namespace ItemAsset
{
    // 동료 항해사
    [CreateAssetMenu(fileName = "Navigator", menuName = "Item/Company/Navigator")]
    public class CompanyNavigator : BaseItem
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
