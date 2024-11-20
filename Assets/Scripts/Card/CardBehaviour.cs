using System;
using System.Linq;
using Enemy;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

namespace Card
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class CardBehaviour : MonoBehaviour
    {
        public BaseCard Data { get; private set; }

        public Action<CardBehaviour> OnCardClicked;

        private void Awake()
        {
            Data = null;
        }

        // 카드 첫 세팅, CardManager에서 사용
        public void Init(BaseCard c)
        {
            if (Data is not null) return;
            Data = c;
            name = Data.CardName;
            GetComponent<SpriteRenderer>().sprite = Data.Image;
        }


        // ReSharper disable Unity.PerformanceAnalysis
        /// <summary>
        /// 카드 사용 시 호출
        /// </summary>
        /// <param name="singleTarget">TargetingType이 Single일 때만 넘겨주면 됨</param>
        public void Use(BaseEnemy singleTarget = null)
        {
            Debug.Log($"{Data.CardName} - {singleTarget?.name}");
            // Single 타겟이 필요하나 지정하지 않은 경우
            if (singleTarget == null && Data.TargetingType == TargetingType.Single)
            {
                Debug.LogWarning($"{Data.CardName}: No Target, maybe dead");
                return;
            }

            if (Data.TargetingType == TargetingType.Single)
            {
                Debug.Log($"{Data.CardName} used to {singleTarget.name}");
                Data.Use(singleTarget);
            }
            else
            {
                Data.Use(Managers.Battle.enemyList.ToArray());
            }
        }

        // 카드 오브젝트 클릭 시 Action 실행
        private void OnMouseDown()
        {
            OnCardClicked?.Invoke(this);
        }
    }
}