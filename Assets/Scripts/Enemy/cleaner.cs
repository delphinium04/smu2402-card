using UnityEngine;

public class Cleaner : BaseEnemy
{
    private void Awake()
    {
        hp = 12;
        damage = 6;
        weight = 1;
    }

    public override void AttackPlayer()
    {
        Debug.Log("청소부가 플레이어를 공격합니다. 데미지: " + damage);
        FindObjectOfType<PlayerController>().TakeDamage(damage);
    }

    protected override void OnDie()
    {
        Debug.Log("청소부가 사망했습니다.");
    }
}