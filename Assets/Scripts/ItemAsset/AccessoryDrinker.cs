using Card;
using CardAsset;
using Item;
using UnityEngine;

namespace ItemAsset
{
    // 장신구 쌍칼
    [CreateAssetMenu(fileName = "Drinker", menuName = "Item/Accessory/Drinker")]
    public class AccessoryDrinker : BaseItem
    {
        protected override void Enable()
        {
            for (int i = 1; i <= 3; i++)
                Resources.Load<CardNormalBeer>($"Card/Normal/Beer{i}").hasExtraHeal = true;
        }

        public override void Disable()
        {
            for (int i = 1; i <= 3; i++)
                Resources.Load<CardNormalBeer>($"Card/Normal/Beer{i}").hasExtraHeal = false;
        }
    }
}
