using Card;
using CardAsset;
using Item;
using UnityEngine;

namespace ItemAsset
{
    // 장신구 쌍칼
    [CreateAssetMenu(fileName = "Compass", menuName = "Item/Accessory/Compass")]
    public class AccessoryCompass : BaseItem
    {
        protected override void Enable()
        {
            // GameManager.canAcross = true;
        }

        public override void Disable()
        {
            // GameManager.canAcross = false;
        }
    }
}
