﻿using System.Collections.Generic;
using System.Linq;
using Card;
using Effect;
using Enemy;
using UnityEngine;

namespace CardAsset
{
    [CreateAssetMenu(fileName = "Swordman", menuName = "Card/Special/Swordman")]
    public class CardSpecialSwordman : BaseCard
    {
        [Header("Swordman Variables")] private const int Damage = 25;
        public Sprite upgradedImage;
        public bool isUpgraded = false;

        public override void Use(params BaseEnemy[] targets)
        {
            if (targets.Length == 0)
            {
                Debug.LogError($"{CardName}: No target was assigned!");
                return;
            }

            int damage = isUpgraded ? Damage * 2 : Damage;

            targets[0].TakeDamage(damage);

            if (isUpgraded)
                return;
            PlayerController p = PlayerController.Instance;
            p.TakeDamage(damage);
        }
    }
}