using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

namespace Card
{
    [RequireComponent(typeof(SpriteRenderer))]
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
            if (Card is not null) return;
            Card = c;
            name = Card.CardName;
            GetComponent<SpriteRenderer>().sprite = Card.Image;
        }

        
        // ReSharper disable Unity.PerformanceAnalysis
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

        // 전투 중에 카드 오브젝트 클릭 시 이벤트 함수
        private void OnMouseDown()
        {
            if (BattleManager.Instance != null)
            {
                // BattleManager.Action
            }
        }
    }
}
