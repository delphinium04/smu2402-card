using Card;
using UnityEngine;

namespace CardAsset
{
    [CreateAssetMenu(fileName = "Orange", menuName = "Card/Skill/Orange")]
    public class CardSkillOrange : BaseCard
    {
        public override void Use(params GameObject[] targets)
        {
            Debug.Log($"{CardName} Skill Used");

        }
    }

}
