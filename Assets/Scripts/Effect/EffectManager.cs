using System;
using System.Collections.Generic;
using UnityEngine;

namespace Effect
{
    public class EffectManager
    {
        private readonly List<BaseEffect> _effects;

        public enum Kind
        {
            Debuff,
            Buff,
            All
        };

        private EffectManager()
        {
           _effects = new List<BaseEffect>();
           // gameManager.onTurnIncreased: Action(or Event) += OnTurnIncrease;
        }

        public IReadOnlyList<BaseEffect> GetEffects(Kind kind = Kind.All)
        {
            switch (kind)
            {
                case Kind.Debuff:
                    break;
                case Kind.Buff:
                    break;
                case Kind.All:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(kind), kind, null);
            }
            return _effects.AsReadOnly();
        }

        public void AddEffect(BaseEffect effect)
        {
            _effects.Add(effect);
        }
        
        public void RemoveEffect(BaseEffect effect)
        {
            if(_effects.Contains(effect))
            { 
                _effects.Remove(effect);
                effect.Remove();
            }
            else Debug.LogError("EffectManager: failed to remove effect (no effect)");
        }
        
        public void OnTurnIncreased()
        {
            foreach(var e in _effects)
                e.IncreaseTurn();
        }
    }
}
