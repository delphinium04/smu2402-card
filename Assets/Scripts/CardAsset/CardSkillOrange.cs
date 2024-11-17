using System.Collections.Generic;
using Card;
using Effect;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace CardAsset
{
    [CreateAssetMenu(fileName = "Orange", menuName = "Card/Skill/Orange")]
    public class CardSkillOrange : BaseCard
    {
        [Header("Orange Variables")]
        [SerializeField]
        public bool isUpgraded = false;
        
        public override void Use(params GameObject[] targets)
        {
            PlayerController p = PlayerController.Instance;

            if (!GetComponent(p.gameObject, out EffectManager em)) return;

            var list = em.GetEffects(EffectManager.Kind.Debuff);
            if(list.Count > 1 && isUpgraded)
            {
                em.RemoveEffect(list[0]);
                em.RemoveEffect(list[1]);
            }
            else if (list.Count > 0)
                em.RemoveEffect(list[0]);
        }
    }

}
