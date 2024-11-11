using System;
using Card;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CardAsset
{
    [CreateAssetMenu(fileName = "Gun", menuName = "Card/Normal/Gun")]
    public class CardNormalGun : BaseCard
    {
        private const int Damage = 3;
        public bool HasExtraDamage = false;

        public override void Use(params GameObject[] targets)
        {
            if (targets.Length == 1)
            {
                Debug.LogError("CardGeneralGun: No target to cast!");
                return;
            }
            
            var damage = Damage;
            if (HasExtraDamage) damage++;

            switch (CardLevel)
            {
                case CardLevel.One:
                    
                    Debug.Log($"{CardName}: {targets[Random.Range(0, targets.Length)].name} was Damaged {damage}");
                    Debug.Log($"{CardName}: {targets[Random.Range(0, targets.Length)].name} was Damaged {damage}");
                    break;
                case CardLevel.Two:
                    Debug.Log($"{CardName}: {targets[Random.Range(0, targets.Length)].name} was Damaged {damage * 2}");
                    Debug.Log($"{CardName}: {targets[Random.Range(0, targets.Length)].name} was Damaged {damage * 2}");
                    break;
                case CardLevel.Three:
                    for (int i = 0; i < 3; i++)
                    {
                        Debug.Log($"{CardName}: {targets[Random.Range(0, targets.Length)].name} was Damaged {damage * 2} +  debuff turn increased");
                    }
                    break;
                default:
                    Debug.LogError("CardGeneralGun: Unknown CardLevel");
                    break;
            }

        }
    }

}
