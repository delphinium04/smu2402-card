using UnityEngine;

namespace CardAsset
{
    [CreateAssetMenu(fileName = "Group", menuName = "Card/Special/Group")]
    public class CardSpecialGroup : Card
    {
        private const int Damage = 5;

        public override void Use(params GameObject[] targets)
        {
            Debug.Log($"{CardName} SpecialCard used");
        }
    }
}
