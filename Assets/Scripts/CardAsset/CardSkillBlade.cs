using UnityEngine;

namespace CardAsset
{
    [CreateAssetMenu(fileName = "Blade", menuName = "Card/Skill/Blade")]
    public class CardSkillBlade : Card
    {
        public override void Use(params GameObject[] targets)
        {
            Debug.Log($"{CardName} Skill Used");
        }
    }

}
