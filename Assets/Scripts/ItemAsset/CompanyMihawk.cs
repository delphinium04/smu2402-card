using Item;
using UnityEngine;

namespace ItemAsset
{
    // 동료 항해사
    [CreateAssetMenu(fileName = "Mihawk", menuName = "Item/Company/Mihawk")]
    public class CompanyMihawk : BaseItem
    {
        protected override void Enable()
        {
            return;
        }

        public override void Disable()
        {
            return;
        }
    }
}
