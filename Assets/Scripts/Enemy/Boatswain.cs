using Entity;
using UnityEngine;

namespace Enemy
{
    public class Boatswain : BaseEnemy
    {
        [SerializeField]
        private GameObject cleanerPrefab; // 청소부 프리팹을 참조하는 변수
        private bool isCleaner = false;

        void SummonCleaner()
        {
            Debug.Log($"{data.name}: 청소부 소환 + Prefab Resources 재조정");

            // 프리팹을 소환하기 위한 위치 설정 (해당 적의 동적 생성 구현 후 다시 구현하기)
            Vector2 spawnPosition = new Vector2(1, 0); // 현재 위치 기준으로 약간 오른쪽에 생성 ( + transform.position)

            Instantiate(cleanerPrefab, spawnPosition, Quaternion.identity);
        }

        public override void ActivatePattern()
        {
            Debug.Log($"{GetType()}: 2회 공격");
            
            PlayerController.Instance.TakeDamage(Atk);
            PlayerController.Instance.TakeDamage(Atk);

            if (Hp > 30 || isCleaner) return;
            SummonCleaner(); // 체력이 30 이하로 내려갈 경우 특수 행동
            isCleaner = true;
        }
    }
}