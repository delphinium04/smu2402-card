using UnityEngine;

namespace Enemy
{
    public class Boatswain : BaseEnemy
    {
        EnemyData _cleanerPrefab; // 청소부 프리팹을 참조하는 변수
        bool _isCleaner = false;
        
        void SummonCleaner()
        {
            Debug.Log($"{data.name}: 청소부 소환 + Prefab Resources 재조정");

            // 프리팹을 소환하기 위한 위치 설정 (해당 적의 동적 생성 구현 후 다시 구현하기)
            var spawnPosition = transform.position + Vector3.right * 1.5f; // 현재 위치 기준으로 약간 오른쪽에 생성 ( + transform.position)
            var e = Managers.Resource.GetEnemy(Managers.Resource.Load<EnemyData>("Enemy/Cleaner"));
            e.transform.position = spawnPosition;
            Managers.Battle.AddEnemy(e);
        }

        public override void ActivatePattern()
        {
            Debug.Log($"{GetType()}: 2회 공격");
            
            PlayerController.Instance.TakeDamage(Atk);
            PlayerController.Instance.TakeDamage(Atk);

            if (Hp > 30 || _isCleaner) return;
            SummonCleaner(); // 체력이 30 이하로 내려갈 경우 특수 행동
            _isCleaner = true;
        }
    }
}