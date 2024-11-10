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
        Debug.Log("���� ������ �÷��̾ �����մϴ�. ������: " + damage);
        FindObjectOfType<PlayerController>().TakeDamage(damage);
    }

    protected override void OnDie()
    {
        Debug.Log("���� ������ ����߽��ϴ�.");
    }
}