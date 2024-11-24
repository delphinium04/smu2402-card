using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Effect
{
    /// <summary>
    /// 각 엔티티가 가지고 있어야 하는 컴포넌트입니다
    /// 스킬 카드가 {Entity}.{EffectManagerInstance}.Add(Effect)~ 이런 식으로 접근하며
    /// 각 효과의 턴 당 행동을 위해 게임의 턴 증가 이벤트에 등록해야 합니다
    /// </summary>
    public class EffectManager : MonoBehaviour
    {
        private const int _debuffStartIndexInType = 3; // from BaseEffect.Type
        public List<BaseEffect> _effects = new List<BaseEffect>();

        public Action<BaseEffect> OnEffectAdded = null;
        public Action<BaseEffect> OnEffectRemoved = null;

        // For Accessory
        public bool HasCross;

        public enum Kind
        {
            Debuff,
            Buff,
            All
        };

        void Start()
        {
            Managers.Battle.OnTurnPassed += OnTurnPassed;
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
            if (FindEffect(effect) != null) return;

            OnEffectAdded?.Invoke(effect);
            BaseEffect e = effect.Clone();
            _effects.Add(e);
            e.Init(gameObject, turn+1);
        }

        public void RemoveEffect(BaseEffect effect)
        {
            BaseEffect e = FindEffect(effect);
            if (e == null) return;

            OnEffectRemoved?.Invoke(e);
            _effects.Remove(e);
            e.Remove();
        }

        private void OnTurnPassed()
        {
            var list = _effects.ToArray();
            foreach (var effect in list)
            {
                if (effect != null)
                    effect.OnTurnPassed();
            }
        }

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

            if (target != null)
            {
                BaseEffect e = FindEffect(target);
                if (e == null) return;
                if (isIgnored) e.AddIgnoreTurn(turn);
                else e.AddTurn(turn);
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

        private BaseEffect FindEffect(BaseEffect effect)
        {
            foreach (var item in _effects)
            {
                if (item.Type == effect.Type) return item;
            }

            return null;
        }

        private static bool IsBuff(BaseEffect e)
            => (int)e.Type >= _debuffStartIndexInType;
    }
}