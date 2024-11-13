using System.Collections.Generic;
using Card;
using Effect;
using UnityEngine;

namespace CardAsset
{
    [CreateAssetMenu(fileName = "FoodPoison", menuName = "Card/Skill/FoodPoison")]
    public class CardSkillFoodPoison : BaseCard
    {
        [Header("FoodPoison Variables")]
        public bool isUpgraded = false;

        public override void Use(params GameObject[] targets)
        {
            if (targets.Length == 0)
            {
                Debug.LogError($"{CardName}: No target");
                return;
            }

            if (!GetComponent(targets[Random.Range(0, targets.Length)], out EffectManager em)) return;

            var effectList = em.GetEffects(EffectManager.Kind.Buff);
            if (effectList?.Count > 0)
                em.RemoveEffect(effectList[Random.Range(0, effectList.Count)]);

            effectList = em.GetEffects(EffectManager.Kind.Buff);
            if (!isUpgraded || effectList.Count == 0) return;
            
            em.RemoveEffect(effectList[Random.Range(0, effectList.Count)]);
        }
    }
}
