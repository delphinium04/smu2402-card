using Card;
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

        public override void Use(params GameObject[] targets)
        {
            if(targets.Length > 1) Debug.LogWarning($"{CardName}: There is more than one target");
            int damageValue = Damage;
            if (CardLevel == CardLevel.Two) damageValue = Damage * 2;
            if (CardLevel == CardLevel.Three)
            {
                damageValue = Damage * 3;
                // EnemyCounts == 1 -> Damage *= 1.5
            }
            if (hasExtraDamage) damageValue++;
            
            if(GetComponent(targets[0], out BaseEnemy enemy))
                enemy.TakeDamage(damageValue);
        }
    }
}
