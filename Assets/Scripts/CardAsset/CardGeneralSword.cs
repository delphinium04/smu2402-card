using Card;
using UnityEngine;

namespace CardAsset
{
    [CreateAssetMenu(fileName = "Sword", menuName = "Card/General/Sword")]
    public class CardGeneralSword : BaseCard
    {
        private const int Damage = 5;

        public override void Use(params GameObject[] targets)
        {
            switch (CardLevel)
            {
                case CardLevel.One:
                    Debug.Log($"{CardName} Damaged {Damage}");
                    break;
                case CardLevel.Two:
                    Debug.Log($"{CardName} Damaged {Damage * 2} Level 2");
                    break;
                case CardLevel.Three:
                    Debug.Log($"{CardName} Damaged {Damage * 3} + Level 3");
                    break;
                default:
                    Debug.LogError("ERROR");
                    break;
            }

        }
    }
}
