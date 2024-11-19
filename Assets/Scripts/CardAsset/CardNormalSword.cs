using Card;
using Enemy;
using UnityEngine;

namespace CardAsset
{
    [CreateAssetMenu(fileName = "Sword", menuName = "Card/Normal/Sword")]
    public class CardNormalSword : BaseCard
    {
        [Header("Sword Variables")]
        [SerializeField]
        private CardLevel cardLevel;
        public CardLevel CardLevel => cardLevel;
        private const int Damage = 5;
        public bool hasExtraDamage = false; // Accessory Double Sword

        public override void Use(params BaseEnemy[] targets)
        {
            int damageValue = Damage;
            switch (CardLevel)
            {
                case CardLevel.Two:
                    damageValue = Damage * 2;
                    break;
                case CardLevel.Three:
                    damageValue = Damage * 3;
                    // EnemyCounts == 1 -> Damage *= 1.5
                    break;
            }

            if (hasExtraDamage) damageValue++;
            
            damageValue = (int)(damageValue * (PlayerController.Instance.AtkMultiplier / 100.0f));
            targets[0].TakeDamage(damageValue);
        }
    }
}
