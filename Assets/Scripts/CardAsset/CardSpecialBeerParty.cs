using UnityEngine;

namespace CardAsset
{
    [CreateAssetMenu(fileName = "BeerParty", menuName = "Card/Special/BeerParty")]
    public class CardSpecialBeerParty : Card
    {
        private const int Damage = 5;

        public override void Use(params GameObject[] targets)
        {
            Debug.Log($"{CardName} SpecialCard used");
        }
    }
}
