using UnityEngine;

public class PirateCaptain : BaseEnemy
{
    private void Awake()
    {
        hp = 80;
        damage = 20;
        weight = 4;
    }

    public override void AttackPlayer()
    {
        Debug.Log("해적 선장이 플레이어를 공격합니다. 데미지: " + damage);
        FindObjectOfType<PlayerController>().TakeDamage(damage);
    }

    protected override void OnDie()
    {
        Debug.Log("해적 선장이 사망했습니다.");
    }
}