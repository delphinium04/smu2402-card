﻿using Card;
using UnityEngine;

namespace CardAsset
{
    [CreateAssetMenu(fileName = "BuffNaming", menuName = "Card/Skill/BuffNaming")]
    public class CardSkillBuffNaming : BaseCard
    {
        public override void Use(params GameObject[] targets)
        {
            Debug.Log($"{CardName} Skill Used");
        }
    }
}