using System.Linq;
using Card;
using Enemy;
using Entity;
using UnityEngine;

namespace CardAsset
{
    [CreateAssetMenu(fileName = "BeerParty", menuName = "Card/Special/BeerParty")]
    public class CardSpeicalBeerParty : BaseCard
    {
        [Header("BeerParty Variables")] 
        private const int HealAmount = 15;
        public Sprite upgradedImage;
        public bool isUpgraded = false;

        public override void Use(params GameObject[] targets)
        {
            if (targets.Length == 0)
            {
                Debug.LogError($"{CardName}: No target was assigned!");
                return;
            }

            PlayerController p = PlayerController.Instance;
            if (isUpgraded)
            {
                p.Heal(HealAmount * 2);
                return;
            }

            p.Heal(HealAmount);
            targets.ToList().ForEach(target =>
            {
                if (GetComponent(target, out BaseEnemy enemy))
                    return;
                enemy.Heal(HealAmount);
            });
        }
    }
}