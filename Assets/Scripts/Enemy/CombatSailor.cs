using UnityEngine;

public class CombatSailor : BaseEnemy
{
    private void Awake()
    {
        hp = 35;
        damage = 12;
        weight = 2;
    }

    public override void AttackPlayer()
    {
        Debug.Log("전투 선원이 플레이어를 공격합니다. 데미지: " + damage);
        FindObjectOfType<PlayerController>().TakeDamage(damage);
    }

    protected override void OnDie()
    {
        Debug.Log("전투 선원이 사망했습니다.");
    }
}