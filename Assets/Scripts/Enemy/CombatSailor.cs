using Enemy;
using UnityEngine;

namespace Enemy
{
    public class CombatSailor : BaseEnemy
    {
        private int attackCount = 0; // 공격 횟수 추적

        public override void ActivatePattern()
        {
            attackCount++;

            if (attackCount % 3 == 0) // 3번째 공격마다 속박 효과 부여
            {
                Debug.Log($"{data.name}이(가) 플레이어에게 속박을 부여했습니다. 플레이어는 이번 턴 동안 일반 카드를 사용할 수 없습니다.");
                // PlayerController.Instance.canSelectNormalCard = true;
                return;
            }

            PlayerController.Instance.TakeDamage(Atk);
        }
    }
}