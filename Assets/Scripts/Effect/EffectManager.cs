using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

namespace Effect
{
    /// <summary>
    /// 각 엔티티가 가지고 있어야 하는 컴포넌트입니다
    /// 스킬 카드가 {Entity}.{EffectManagerInstance}.Add(Effect)~ 이런 식으로 접근하며
    /// 각 효과의 턴 당 행동을 위해 게임의 턴 증가 이벤트에 등록해야 합니다
    /// </summary>
    public class EffectManager : MonoBehaviour
    {
        private const int DebuffStartIndexInType = 3; // from BaseEffect.Type
        private readonly List<BaseEffect> _effects;

        // For Accessory
        public bool HasCross;

        public enum Kind
        {
            Debuff,
            Buff,
            All
        };

        private EffectManager()
        {
            _effects = new List<BaseEffect>();
            BattleManager.Instance.OnTurnPassed += OnTurnPassed;
        }

        public IReadOnlyList<BaseEffect> GetEffects(Kind kind = Kind.All)
        {
            return kind switch
            {
                Kind.All => _effects,
                Kind.Debuff => _effects.Where(e => !IsBuff(e)).ToList(),
                Kind.Buff => _effects.Where(e => !IsBuff(e)).ToList(),
                _ => throw new ArgumentOutOfRangeException(nameof(kind), kind, null)
            };
        }

        public void AddEffect(BaseEffect effect, int turn)
        {
            _effects.Add(effect);
            effect.Init(gameObject, turn);
        }

        public void RemoveEffect(BaseEffect effect)
        {
            if (_effects.Contains(effect))
            {
                _effects.Remove(effect);
                effect.Remove();
            }
            else Debug.LogError("EffectManager: no effect in list");
        }

        private void OnTurnPassed()
            => _effects.ForEach(e => e.OnTurnPassed());

        /// <summary>
        /// 효과의 턴을 관리합니다
        /// </summary>
        /// <param name="kind">Buff or Debuff, All</param>
        /// <param name="turn">Duration</param>
        /// <param name="isIgnored">Add Ignore Turn?</param>
        /// <param name="target">Especially</param>
        public void AddEffectTurn(Kind kind, int turn, bool isIgnored, BaseEffect target = null)
        {
            if (kind == Kind.Buff && HasCross)
                turn += 1;
            
            if (target != null && _effects.Contains(target))
            {
                if (isIgnored) target.AddIgnoreTurn(turn);
                else target.AddTurn(turn);
                return;
            }

            switch (kind)
            {
                case Kind.Debuff:
                    _effects.ForEach(e =>
                    {
                        if (!IsBuff(e))
                            if (isIgnored) e.AddIgnoreTurn(turn);
                            else e.AddTurn(turn);
                    });
                    break;
                case Kind.Buff:
                    _effects.ForEach(e =>
                    {
                        if (IsBuff(e))
                            if (isIgnored) e.AddIgnoreTurn(turn);
                            else e.AddTurn(turn);
                    });
                    break;
                case Kind.All:
                    _effects.ForEach(e =>
                    {
                        if (isIgnored) e.AddIgnoreTurn(turn);
                        else e.AddTurn(turn);
                    });
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(kind), kind, null);
            }
        }

        private static bool IsBuff(BaseEffect e)
            => (int)e.Type >= DebuffStartIndexInType;
    }
}
