using UnityEngine;

public class Boatswain : BaseEnemy
{
    private void Awake()
    {
        hp = 60;
        damage = 9;
        weight = 3;
    }

    public override void AttackPlayer()
    {
        Debug.Log("갑판장이 플레이어를 공격합니다. 데미지: " + damage);
        FindObjectOfType<PlayerController>().TakeDamage(damage);
    }

    protected override void OnDie()
    {
        Debug.Log("갑판장이 사망했습니다.");
    }
}