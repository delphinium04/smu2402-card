using System.Linq;
using Card;
using Effect;
using UnityEngine;

namespace CardAsset
{
    [CreateAssetMenu(fileName = "GroupLife", menuName = "Card/Special/GroupLife")]
    public class CardSpecialGroupLife : BaseCard
    {
        [Header("GroupLife Variables")]
        [SerializeField]
        private BaseEffect effect1;
        [SerializeField]
        private BaseEffect effect2;
        public bool isUpgraded = false;

        public override void Use(params GameObject[] targets)
        {
            if (targets.Length == 0)
            {
                Debug.LogError($"{CardName}: No target was assigned!");
                return;
            }

            if (effect1 == null || effect2 == null)
            {
                Debug.LogError($"{CardName}: No effect was assigned!");
                return;
            }

            int turn = isUpgraded ? 2 : 1;

            targets.ToList().ForEach(target =>
            {
                if (!GetComponent(target, out EffectManager em))
                    return;
                em.AddEffect(effect1, turn);
                em.AddEffect(effect2, turn);
            });

            if (isUpgraded) return;
            PlayerController p = PlayerController.Instance;
            if (!GetComponent(p.gameObject, out EffectManager em)) return;
            em.AddEffect(effect1, turn);
            em.AddEffect(effect2, turn);
        }
    }
}
