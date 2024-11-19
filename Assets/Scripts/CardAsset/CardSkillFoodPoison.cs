using System.Collections.Generic;
using System.Linq;
using Card;
using Effect;
using Enemy;
using UnityEngine;

namespace CardAsset
{
    [CreateAssetMenu(fileName = "FoodPoison", menuName = "Card/Skill/FoodPoison")]
    public class CardSkillFoodPoison : BaseCard
    {
        [Header("FoodPoison Variables")] 
        public Sprite upgradedImage;
        public bool isUpgraded = false;


        public override void Use(params BaseEnemy[] targets)
        {
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