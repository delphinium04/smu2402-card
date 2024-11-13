using Card;
using CardAsset;
using Effect;
using Item;
using UnityEngine;

namespace ItemAsset
{
    // 장신구 쌍칼
    [CreateAssetMenu(fileName = "Cross", menuName = "Item/Accessory/Cross")]
    public class AccessoryCross : BaseItem
    {
        protected override void Enable()
        {
            PlayerController p = FindObjectOfType<PlayerController>();
            p.GetComponent<EffectManager>().HasCross = true;
        }

        public override void Disable()
        {
            PlayerController p = FindObjectOfType<PlayerController>();
            p.GetComponent<EffectManager>().HasCross = false;
        }
    }
}
