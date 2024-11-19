using System.Collections.Generic;
using Card;
using Effect;
using Enemy;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace CardAsset
{
    [CreateAssetMenu(fileName = "Orange", menuName = "Card/Skill/Orange")]
    public class CardSkillOrange : BaseCard
    {
        [Header("Orange Variables")]
        public Sprite upgradedImage;
        public bool isUpgraded = false;

        public override void Use(params BaseEnemy[] targets)
        {
            PlayerController p = PlayerController.Instance;

            if (!GetComponent(p, out EffectManager em)) return;

            var list = em.GetEffects(EffectManager.Kind.Debuff);
            switch (list.Count)
            {
                case > 1 when isUpgraded:
                    em.RemoveEffect(list[0]);
                    em.RemoveEffect(list[1]);
                    break;
                case > 0:
                    em.RemoveEffect(list[0]);
                    break;
            }
        }
    }
}