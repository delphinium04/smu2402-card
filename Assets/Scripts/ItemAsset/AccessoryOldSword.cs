using Card;
using CardAsset;
using Item;
using UnityEngine;

namespace ItemAsset
{
    // 장신구 쌍칼
    [CreateAssetMenu(fileName = "OldSword", menuName = "Item/Accessory/OldSword")]
    public class AccessoryOldSword : BaseItem
    {
        protected override void Enable()
        {
            // pass
        }

        public override void Disable()
        {
            // pass
        }
    }
}
