using System.Collections.Generic;
using System.Linq;
using Card;
using Effect;
using Enemy;
using UnityEngine;

namespace CardAsset
{
    [CreateAssetMenu(fileName = "Scurvy", menuName = "Card/Special/Scurvy")]
    public class CardSpecialScurvy : BaseCard
    {
        [Header("Scurvy Variables")] private const int Damage = 10;
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
            targets.ToList().ForEach(target =>
            {
               target.TakeDamage(damage);
            });

            PlayerController p = PlayerController.Instance;

            if (!isUpgraded)
                p.TakeDamage(damage);
        }
    }
}