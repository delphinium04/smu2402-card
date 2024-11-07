using Card;
using UnityEngine;

namespace CardAsset
{
    [CreateAssetMenu(fileName = "PoisonSword", menuName = "Card/Skill/PoisonSword")]
    public class CardSkillPoisonSword : BaseCard
    {
        public override void Use(params GameObject[] targets)
        {
            Debug.Log($"{CardName} Skill Used");
        }
    }
}