using Card;
using CardAsset;
using Item;
using UnityEngine;

namespace ItemAsset
{
    // 장신구 쌍칼
    [CreateAssetMenu(fileName = "AccessoryDoubleSword", menuName = "Item/Accessory/DoubleSword")]
    public class AccessoryDoubleSword : BaseItem
    {
        protected override void Enable()
        {
            for (int i = 1; i <= 3; i++)
                Resources.Load<CardNormalGun>($"Card/Normal/Gun{i}").HasExtraDamage = true;
        }

        public override void Disable()
        {
            for (int i = 1; i <= 3; i++)
                Resources.Load<CardNormalGun>($"Card/Normal/Gun{i}").HasExtraDamage = false;
        }
    }
}
