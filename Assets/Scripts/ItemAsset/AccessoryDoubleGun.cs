using Card;
using CardAsset;
using Item;
using UnityEngine;

namespace ItemAsset
{
    // 장신구 쌍칼
    [CreateAssetMenu(fileName = "DoubleGun", menuName = "Item/Accessory/DoubleGun")]
    public class AccessoryDoubleGun : BaseItem
    {
        protected override void Enable()
        {
            for (int i = 1; i <= 3; i++)
                Resources.Load<CardNormalGun>($"Card/Normal/Gun{i}").hasExtraDamage = true;
        }

        public override void Disable()
        {
            for (int i = 1; i <= 3; i++)
                Resources.Load<CardNormalGun>($"Card/Normal/Gun{i}").hasExtraDamage = false;
        }
    }
}
