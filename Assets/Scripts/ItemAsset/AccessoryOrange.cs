using Card;
using CardAsset;
using Item;
using UnityEngine;

namespace ItemAsset
{
    [CreateAssetMenu(fileName = "Orange", menuName = "Item/Accessory/Orange")]
    public class AccessoryOrange : BaseItem
    {
        protected override void Enable()
        {
            // Player.maxHP += 15;
        }

        public override void Disable()
        {
            // Player.MaxHP -= 15;
        }
    }
}
