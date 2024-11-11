using Item;
using UnityEngine;

namespace ItemAsset
{
    // 동료 항해사
    [CreateAssetMenu(fileName = "CompanyNavigator", menuName = "Item/Company/Navigator")]
    public class CompanyNavigator:BaseItem
    {
        protected override void Enable()
        {
            // TargetController?.IgnoreEnvironmentTurns += 3;
            return;
        }

        public override void Disable()
        {
            return;
        }
    }
}
