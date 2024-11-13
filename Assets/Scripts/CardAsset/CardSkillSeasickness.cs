using System.Collections.Generic;
using Card;
using Effect;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace CardAsset
{
    [CreateAssetMenu(fileName = "Seasickness", menuName = "Card/Skill/Seasickness")]
    public class CardSkillSeasickness : BaseCard
    {
        [Header("Seasickness Variables")]
        [SerializeField]
        private BaseEffect effect;
        public bool isUpgraded = false;

        public override void Use(params GameObject[] targets)
        {
            if (targets.Length == 0)
            {
                Debug.LogError($"{CardName}: No target");
                return;
            }

            if (effect == null)
            {
                Debug.LogError($"{CardName}: Effect resource is not found!");
                return;
            }

            if (!GetComponent(targets[0], out EffectManager em)) return;
            em.AddEffect(effect, isUpgraded ? 2 : 1);
        }
    }

}
