// https://programmingdev.com/unity-%EC%9C%A0%EB%8B%88%ED%8B%B0-%EC%BD%94%EB%A3%A8%ED%8B%B4-%EB%B2%84%ED%94%84-%EB%B0%8F-%EB%94%94%EB%B2%84%ED%94%84-%EC%8B%9C%EC%8A%A4%ED%85%9C-%EA%B5%AC%ED%98%84solid-%EC%9B%90%EC%B9%99-%EC%A4%80/
using UnityEngine;

namespace Effect
{
    /// <summary>
    /// 적용된 엔티티 관점 기준 버프 -> 디버프순
    /// [철갑, 분노, 면역, | 약화, 취약, 침묵, 속박]
    /// </summary>
    public enum Type
    {
        Armor,
        Rage,
        Immunity,
        Weakness,
        Fatigue,
        Silence,
        Root
    }


    public abstract class BaseEffect : ScriptableObject
    {
        [SerializeField]
        protected Type type;
        public Type Type => type;

        [SerializeField]
        protected string effectName;
        public string EffectName => effectName;

        [SerializeField]
        protected Sprite effectIcon;
        public Sprite EffectIcon => effectIcon;

        [SerializeField]
        protected string description;
        public string Description => description;

        protected GameObject Target;
        protected int TurnDuration;
        protected int TurnIgnored; // disable effect temporarily

        // Assign variable and Apply effect
        public void Init(GameObject target, int duration)
        {
            if (Target != null)
            {
                Debug.LogError($"{GetType().Name}.Init: Target already set!");
                return;
            }

            Target = target;
            TurnDuration = duration;
            TurnIgnored = 0;

            Apply();
            // Target.EffectManager.AddEffect(this);
        }
        
        protected abstract void Apply();

        // Remove effect from entity
        public abstract void Remove();
        public abstract void OnTurnPassed();
        // Remove effect temporarily during N turns
        public abstract void IgnoreTurn(int turn);
        public abstract void AddTurn(int turn);
    }
}