using Card;
using UnityEngine;

namespace CardAsset
{
    [CreateAssetMenu(fileName = "Beer", menuName = "Card/General/Beer")]
    public class CardGeneralBeer : BaseCard
    {
        private const int Heal = 4;

        public override void Use(params GameObject[] targets)
        {
            switch (CardLevel)
            {
                case CardLevel.One:
                    Debug.Log($"{CardName} Healed {Heal}");
                    break;
                case CardLevel.Two:
                    Debug.Log($"{CardName} Healed {Heal * 2} Level 2");
                    break;
                case CardLevel.Three:
                    Debug.Log($"{CardName} Healed {Heal * 3} Level 3 ");
                    break;
                default:
                    Debug.LogError("ERROR");
                    break;
            }

        }
    }
}
