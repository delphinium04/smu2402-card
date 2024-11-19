using System.Collections.Generic;
using Card;
using Effect;
using Enemy;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace CardAsset
{
    [CreateAssetMenu(fileName = "Gun", menuName = "Card/Normal/Gun")]
    public class CardNormalGun : BaseCard
    {
        [Header("Gun Variables")]
        [SerializeField]
        private CardLevel cardLevel;
        public CardLevel CardLevel => cardLevel;
        private const int Damage = 3;
        private const int EnemyCount = 2;
        public bool hasExtraDamage = false; // Accessory Double Gun

        public override void Use(params BaseEnemy[] targets)
        {
            int damage = Damage;
            int enemyCount = EnemyCount;

            switch (CardLevel)
            {
                case CardLevel.Two:
                    damage = Damage * 2;
                    break;
                case CardLevel.Three:
                    damage = Damage * 3;
                    enemyCount = EnemyCount + 1;
                    break;
            }

            if (hasExtraDamage) damage++;

            damage = (int)(damage * (PlayerController.Instance.AtkMultiplier / 100.0f));
         
            List<BaseEnemy> alreadyList = new List<BaseEnemy>();
            for (int i = 0, index = Random.Range(0, targets.Length); i < enemyCount; i++, index = Random.Range(0, targets.Length))
            {
                BaseEnemy enemy = targets[index];
                enemy.TakeDamage(damage);
                if (CardLevel != CardLevel.Three || alreadyList.Contains(enemy)) continue;
                
                if(GetComponent(enemy, out EffectManager em))
                    em.AddEffectTurn(EffectManager.Kind.Debuff, 1, false);
                alreadyList.Add(enemy);
            }
        }
    }
}
