using Card;
using Effect;
using Enemy;
using UnityEngine;

namespace CardAsset
{
    [CreateAssetMenu(fileName = "Beer", menuName = "Card/Normal/Beer")]
    public class CardNormalBeer : BaseCard
    {
        [Header("Beer Variables")]
        [SerializeField]
        private CardLevel cardLevel;
        public CardLevel CardLevel => cardLevel;
        private const int HealAmount = 6;
        public bool hasExtraHeal = false; // Accessory 애주가

        public override void Use(params BaseEnemy[] targets)
        {
            int healAmount = HealAmount;
            PlayerController p = PlayerController.Instance;

            if (CardLevel == CardLevel.Two) healAmount = HealAmount * 2;
            if(cardLevel == CardLevel.Three)
            {
                healAmount = HealAmount * 3;
                if(GetComponent(p, out EffectManager em)) 
                    em.AddEffectTurn(EffectManager.Kind.Buff, 1, false);
            }
            
            if(hasExtraHeal) healAmount += 2;            
            p.Heal(healAmount);
        }
    }
}
