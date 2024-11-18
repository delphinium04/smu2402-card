using UnityEngine;

namespace Card
{
    /// <summary>
    /// 카드의 추상 클래스로 데이터의 모음입니다.
    /// 카드의 실체를 참조할 땐 웬만하면 CardBehaviour를 참조해주세요.
    /// </summary>
    public abstract class BaseCard : ScriptableObject
    {
        [SerializeField]
        private string cardName;
        public string CardName => cardName;

        [SerializeField]
        private Sprite image;
        public Sprite Image => image;

        [SerializeField]
        private CardType cardType;
        public CardType CardType => cardType;
       
        [SerializeField]
        private TargetingType targetingType;
        public TargetingType TargetingType => targetingType;

        /// <returns>True when used</returns>
        public abstract void Use(params GameObject[] targets);

        protected static bool GetComponent<T>(GameObject gameObject, out T component)
        {
            if (!gameObject.TryGetComponent(out component))
                return true;
            Debug.LogError($"GameObject {gameObject.name} does not have {component.GetType().Name}");
            return false;
        }
    }
}
