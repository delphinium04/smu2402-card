using Card;
using CardAsset;
using Item;
using UnityEngine;

namespace ItemAsset
{
    // 장신구 쌍칼
    [CreateAssetMenu(fileName = "Trump", menuName = "Item/Accessory/Trump")]
    public class AccessoryTrump : BaseItem
    {
        protected override void Enable()
        {
            // Player.DrawCount += 1;
        }

        public override void Disable()
        {
            // Player.DrawCount -= 1;
        }
    }
}
