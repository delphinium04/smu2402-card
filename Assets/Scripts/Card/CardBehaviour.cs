using UnityEngine;

namespace Card
{
    public class CardBehaviour : MonoBehaviour
    {
        public BaseCard Card { get; private set; }

        private void Awake()
        {
            Card = null;
        }

        // 카드 첫 세팅, CardManager에서 사용
        public void Init(BaseCard c)
        {
            if (Card != null) return;
            Card = c;
            name = Card.CardName;
            // Set texts and Image
        }

        // 카드 사용 시 호출
        public void Use(params GameObject[] targets)
        {
            if (Card.TargetingType != TargetingType.None && targets.Length == 0)
            {
                Debug.LogError($"{Card.CardName} Need targets to use Card");
                return;
            }
            Card.Use(targets);
        }
    }
}
