using Card;
using Effect;
using UnityEngine;

namespace CardAsset
{
    [CreateAssetMenu(fileName = "DrinkPower", menuName = "Card/Skill/DrinkPower")]
    public class CardSkillDrinkPower : BaseCard
    {
        [Header("DrinkPower Variables")] [SerializeField]
        private BaseEffect effect;

        public Sprite upgradedImage;
        public bool isUpgraded = false;

        public override void Use(params GameObject[] targets)
        {
            if (effect == null)
            {
                Debug.LogError($"{CardName}: Effect resource is not found!");
                return;
            }

            PlayerController p = PlayerController.Instance;
            if (GetComponent(p.gameObject, out EffectManager em)) em.AddEffect(effect, 1 + (isUpgraded ? 1 : 0));
        }
    }
}