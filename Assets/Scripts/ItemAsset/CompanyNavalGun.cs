using Item;
using UnityEngine;

namespace ItemAsset
{
    // 동료 항해사
    [CreateAssetMenu(fileName = "NavalGun", menuName = "Item/Company/NavalGun")]
    public class CompanyNavalGun : BaseItem
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
