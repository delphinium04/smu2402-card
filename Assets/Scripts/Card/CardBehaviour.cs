using System;
using System.Collections;
using Enemy;
using UnityEngine;
using DG.Tweening;

namespace Card
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class CardBehaviour : MonoBehaviour
    {
        public BaseCard Data { get; private set; }
        public Action<CardBehaviour> OnCardClicked;

        Vector3 _originPosition = Vector3.forward;
        const float _dotweenDuration = 0.2f;

        private void Awake()
        {
            Data = null;
        }
       
        // 카드 오브젝트 클릭 시 Action 실행
        private void OnMouseDown()
        {
            OnCardClicked?.Invoke(this);

            transform.localScale *= 1.1f;
            transform.DOScale(Vector3.one, _dotweenDuration).SetEase(Ease.OutQuad);
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
            Debug.Log($"{Data.CardName} { singleTarget?.name ?? ""}");
            // Single 타겟이 필요하나 지정하지 않은 경우
            if (singleTarget == null && Data.TargetingType == TargetingType.Single)
            {
                Debug.LogWarning($"{Data.CardName}: No Target, maybe dead");
                return;
            }

            if (Data.TargetingType == TargetingType.Single)
            {
                Debug.Log($"{Data.CardName} used to {singleTarget}");
                Data.Use(singleTarget);
            }
            else
            {
                Data.Use(Managers.Battle.Enemies.ToArray());
            }
        }
        
        public void UnselectCard()
        {
            transform.DOMoveY(_originPosition.y, _dotweenDuration).SetEase(Ease.OutQuad);
        }
        
        public void SelectCard()
        {
            if(_originPosition == Vector3.forward) 
                _originPosition = transform.position;
            transform.DOMoveY(_originPosition.y + 1f, _dotweenDuration).SetEase(Ease.OutQuad);
        }
    }
        
}