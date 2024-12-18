﻿using System.Linq;
using Card;
using Enemy;
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

        public override void Use(params BaseEnemy[] targets)
        {
            PlayerController p = PlayerController.Instance;
            if (isUpgraded)
            {
                p.Heal(HealAmount * 2);
                return;
            }

            p.Heal(HealAmount);
            targets.ToList().ForEach(target =>
            {
                target.Heal(HealAmount);
            });
        }
    }
}