using UnityEngine;

public class WatchfulSailor : BaseEnemy
{
    private void Awake()
    {
        hp = 8;
        damage = 3;
        weight = 1;
    }

    public override void AttackPlayer()
    {
        Debug.Log("망보는 선원이 플레이어를 공격합니다. 데미지: " + damage);
        FindObjectOfType<PlayerController>().TakeDamage(damage);
    }

    protected override void OnDie()
    {
        Debug.Log("감시하는 선원이 사망했습니다.");
    }
}
