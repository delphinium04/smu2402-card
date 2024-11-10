using Card;
using Effect;
using Unity.VisualScripting;
using UnityEngine;

namespace CardAsset
{
    [CreateAssetMenu(fileName = "Blade", menuName = "Card/Skill/Blade")]
    public class CardSkillBlade : BaseCard
    {
        [SerializeField]
        private BaseEffect effect;
        
        public override void Use(params GameObject[] targets)
        {
            if(targets == null)
            {
                Debug.LogError("CardSkillBlade: Target is null!");
                return;
            }
            if(targets.Length > 1)
                Debug.LogWarning("CardSkillBlade: Too many targets!");
            if (effect == null)
            {
                Debug.LogWarning("CardSkillBlade: Effect is null!");
                if ((effect = Resources.Load<BaseEffect>($"Effects/Blade")) == null)
                {
                    Debug.LogError("CardSkillBlade: Effect resource is not found!");
                    return;
                }
            }
            // Targets.EffectManager.AddEffect(effect);
        }
    }

}
