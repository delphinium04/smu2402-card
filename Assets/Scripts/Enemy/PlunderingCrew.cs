using UnityEngine;

public class PlunderingCrew : BaseEnemy
{
    private void Awake()
    {
        hp = 25;
        damage = 10;
        weight = 2;
    }

    public override void AttackPlayer()
    {
        Debug.Log("약탈 선원이 플레이어를 공격합니다. 데미지: " + damage);
        FindObjectOfType<PlayerController>().TakeDamage(damage);
    }

    protected override void OnDie()
    {
        Debug.Log("약탈 승무원이 사망했습니다.");
    }
}