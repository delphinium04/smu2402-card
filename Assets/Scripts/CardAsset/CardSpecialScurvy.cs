using System.Collections.Generic;
using System.Linq;
using Card;
using Effect;
using UnityEngine;

namespace CardAsset
{
    [CreateAssetMenu(fileName = "Scurvy", menuName = "Card/Special/Scurvy")]
    public class CardSpecialScurvy : BaseCard
    {
        [Header("Scurvy Variables")]
        private const int Damage = 10;
        public bool isUpgraded = false;

        public override void Use(params GameObject[] targets)
        {
            if (targets.Length == 0)
            {
                Debug.LogError($"{CardName}: No target was assigned!");
                return;
            }

            int damage = isUpgraded ? Damage * 2 : Damage;
            targets.ToList().ForEach(target =>
            {
                if (GetComponent(target, out BaseEnemy enemy)) enemy.TakeDamage(damage);
            });

            PlayerController p = FindObjectOfType<PlayerController>();

            if (!isUpgraded)
                p.TakeDamage(damage);
        }
    }
}
