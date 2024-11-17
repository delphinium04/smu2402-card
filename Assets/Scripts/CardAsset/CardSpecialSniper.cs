﻿using System.Collections.Generic;
using System.Linq;
using Card;
using Effect;
using UnityEngine;

namespace CardAsset
{
    [CreateAssetMenu(fileName = "Sniper", menuName = "Card/Special/Sniper")]
    public class CardSpecialSniper : BaseCard
    {
        [Header("Sniper Variables")]
        private const int Damage = 2;
        public bool isUpgraded = false;

        public override void Use(params GameObject[] targets)
        {
            if (targets.Length == 0)
            {
                Debug.LogError($"{CardName}: No target was assigned!");
                return;
            }

            var damage = isUpgraded ? Damage + 3 : Damage;

            for (int i = 0; i < 5; i++)
            {
                int randomIndex = Random.Range(0, targets.Length);
                if (GetComponent(targets[randomIndex], out EnemyBehaviour enemy))
                    enemy.TakeDamage(damage);
            }
        }
    }
}
