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

        
        /// <summary>
        /// 카드 사용 시 호출
        /// </summary>
        /// <param name="targets">카드의 사용 대상 (또는 전체)</param>
        public void Use(params GameObject[] targets)
        {
            // 타겟이 필요하나 targets에 아무것도 없는 경우
            if (targets.Length == 0 && Card.TargetingType != TargetingType.None)
            {
                Debug.LogError($"{Card.CardName} Need targets to use Card");
                return;
            }
            Card.Use(targets);
        }
    }
}
