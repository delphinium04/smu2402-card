using Card;
using UnityEngine;

namespace CardAsset
{
    [CreateAssetMenu(fileName = "Blade", menuName = "Card/Skill/Blade")]
    public class CardSkillBlade : BaseCard
    {
        public override void Use(params GameObject[] targets)
        {
            Debug.Log($"{CardName} Skill Used");
        }
    }

}
