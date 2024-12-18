﻿using System.Collections.Generic;
using Card;
using Effect;
using Enemy;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace CardAsset
{
    [CreateAssetMenu(fileName = "Blade", menuName = "Card/Skill/Blade")]
    public class CardSkillBlade : BaseCard
    {
        [Header("Blade Variables")]
        [SerializeField]
        private BaseEffect effect;

        public Sprite upgradedImage;
        public bool isUpgraded = false;

        public override void Use(params BaseEnemy[] targets)
        {
            if (effect == null)
            {
                Debug.LogError($"{CardName}: Effect resource is not found!");
                return;
            }

            PlayerController p = PlayerController.Instance;
            if(GetComponent(p, out EffectManager em))
                em.AddEffect(effect, 1 + (isUpgraded ? 1 : 0));
        }
    }

}
