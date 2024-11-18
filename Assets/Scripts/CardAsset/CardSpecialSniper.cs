using System.Collections.Generic;
using System.Linq;
using Card;
using Effect;
using Enemy;
using UnityEngine;

namespace CardAsset
{
    [CreateAssetMenu(fileName = "Sniper", menuName = "Card/Special/Sniper")]
    public class CardSpecialSniper : BaseCard
    {
        [Header("Sniper Variables")]
        private const int Damage = 2;
        public Sprite upgradedImage;

        public bool isUpgraded = false;

        public override void Use(params GameObject[] targets)
        {
            if (targets.Length == 0)
            {
                Debug.LogError($"{CardName}: No target was assigned!");
                return;
            }

            var damage = isUpgraded ? Damage + 3 : Damage;
            damage = (int)(damage * PlayerController.Instance.AtkMultiplier / 100.0f);

            for (int i = 0; i < 5; i++)
            {
                int randomIndex = Random.Range(0, targets.Length);
                if (GetComponent(targets[randomIndex], out BaseEnemy enemy))
                    enemy.TakeDamage(damage);
            }
        }
    }
}
