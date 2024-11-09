using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

namespace Effect
{
    /// <summary>
    /// 각 엔티티가 가지고 있어야 하는 클래스입니다.
    /// 스킬 카드가 {Entity}.{EffectManagerInstance}.Add(Effect)~ 이런 식으로 접근하며
    /// 각 효과의 턴 당 행동을 위해 게임의 턴 증가 이벤트에 등록해야 합니다.
    /// </summary>
    public class EffectManager
    {
        private const int DebuffStartIndexInType = 3; // from BaseEffect.Type
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
                case Kind.All:
                    return _effects;
                case Kind.Debuff:
                    return _effects.Where(e => !IsBuff(e)).ToList();
                case Kind.Buff:
                    return _effects.Where(e => !IsBuff(e)).ToList();
                default:
                    throw new ArgumentOutOfRangeException(nameof(kind), kind, null);
            }
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
        
        private void OnTurnPassed()
            => _effects.ForEach(e => e.OnTurnPassed());

        public void IgnoreEffect(Kind kind, int turns, BaseEffect target = null)
        {
            if (target != null && _effects.Contains(target))
            {
                target.Ignore(turns);
                return;
            }
            
            switch (kind)
            {
                  case Kind.Debuff:
                      _effects.ForEach(e => { if(IsBuff(e)) e.Ignore(turns);});
                      break;
                  case Kind.Buff:
                      _effects.ForEach(e => { if(!IsBuff(e)) e.Ignore(turns);});
                      break;
                  case Kind.All:
                      _effects.ForEach(e => { e.Ignore(turns);});
                      break;
                  default:
                      throw new ArgumentOutOfRangeException(nameof(kind), kind, null);
            }
        }

        private static bool IsBuff(BaseEffect e)
            => (int)e.Type >= DebuffStartIndexInType;
    }
}
