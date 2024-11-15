using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

namespace Card
{
    public class CardBehaviour : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer cardImage;
        [SerializeField]
        private TMP_Text cardDescription;
        [SerializeField]
        private TMP_Text cardName;
        
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

            cardImage.sprite = Card.Image;
            cardDescription.text = Card.Description;
            cardName.text = Card.CardName;
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

        // 전투 중에 카드 오브젝트 클릭 시 이벤트 함수
        private void OnMouseDown()
        {
            if (BattleManager.Instance != null && BattleManager.Instance.IsPlayerTurn)
            {
                if (BattleManager.Instance.IsCardInUseList(this))
                {
                    // 카드가 이미 선택된 경우, 선택을 취소합니다.
                    BattleManager.Instance.CancelCardSelection(this);
                }
                else
                {
                    // 카드가 아직 선택되지 않은 경우, 카드를 선택합니다.
                    BattleManager.Instance.SelectCardForUse(this);
                }
            }
        }
    }
}
