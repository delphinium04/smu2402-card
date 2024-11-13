using Item;
using UnityEngine;

namespace ItemAsset
{
    // 동료 항해사
    [CreateAssetMenu(fileName = "Doctor", menuName = "Item/Company/Doctor")]
    public class CompanyDoctor : BaseItem
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
