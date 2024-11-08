// https://programmingdev.com/unity-%EC%9C%A0%EB%8B%88%ED%8B%B0-%EC%BD%94%EB%A3%A8%ED%8B%B4-%EB%B2%84%ED%94%84-%EB%B0%8F-%EB%94%94%EB%B2%84%ED%94%84-%EC%8B%9C%EC%8A%A4%ED%85%9C-%EA%B5%AC%ED%98%84solid-%EC%9B%90%EC%B9%99-%EC%A4%80/

using UnityEngine;

namespace Effect
{
    /// <summary>
    /// [취약, 철갑, 분노, 약화, 침묵, 면역, 속박]
    /// </summary>
    public enum Type
    {
        Weakness,
        Armor,
        Rage,
        Fatigue,
        Silence,
        Immunity,
        Root
    }

    public abstract class BaseEffect: ScriptableObject
    {
        [SerializeField]
        protected string name;
        public string Name => name;

        [SerializeField]
        protected Sprite icon;
        public Sprite Icon => icon;

        [SerializeField]
        protected string description;
        public string Description => description;
        
        protected GameObject Target;
        protected Type Type;
        protected int TurnDuration;

        protected BaseEffect(GameObject target, int duration, Type type)
        {
            Target = target;
            TurnDuration = duration;
            Type = type;
        }

        public abstract void Apply();
        public abstract void Remove();
        public abstract void IncreaseTurn();
    }
}
