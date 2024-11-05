using UnityEngine;

namespace CardAsset
{
    [CreateAssetMenu(fileName = "DebuffNaming", menuName = "Card/Skill/DebuffNaming")]
    public class CardSkillDebuffNaming : Card
    {
        public override void Use(params GameObject[] targets)
        {
            Debug.Log($"{CardName} Skill Used");
        }
    }
}