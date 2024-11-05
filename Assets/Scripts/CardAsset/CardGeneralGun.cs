using UnityEngine;

namespace CardAsset
{
    [CreateAssetMenu(fileName = "Gun", menuName = "Card/General/Gun")]
    public class CardGeneralGun : Card
    {
        private const int Damage = 3;

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
                    Debug.Log($"{CardName} Damaged {Damage * 3} Level 3");
                    break;
                default:
                    Debug.LogError("ERROR");
                    break;
            }

        }
    }

}
