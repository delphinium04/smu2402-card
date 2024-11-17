using UnityEngine;

[CreateAssetMenu(fileName = "CombatSailor", menuName = "Enemy/CombatSailor")]
public class CombatSailor : BaseEnemy
{
    private int attackCount = 0; // 공격 횟수 추적

    public override void ActivatePattern(EnemyBehaviour enemy)
    {
        attackCount++;

        if (attackCount % 3 == 0) // 3번째 공격마다 속박 효과 부여
        {
            Debug.Log($"{EnemyName}이(가) 플레이어에게 속박을 부여했습니다. 플레이어는 이번 턴 동안 일반 카드를 사용할 수 없습니다.");
            PlayerController.Instance.canSelectNormalCard = true;
        }
        else
        {
            PlayerController.Instance.TakeDamage(Damage);
        }
    }
}