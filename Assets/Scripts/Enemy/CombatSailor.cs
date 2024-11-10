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
        Debug.Log("���� ������ �÷��̾ �����մϴ�. ������: " + damage);
        FindObjectOfType<PlayerController>().TakeDamage(damage);
    }

    protected override void OnDie()
    {
        Debug.Log("���� ������ ����߽��ϴ�.");
    }
}