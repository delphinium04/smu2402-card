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
        Debug.Log("�������� �÷��̾ �����մϴ�. ������: " + damage);
        FindObjectOfType<PlayerController>().TakeDamage(damage);
    }

    protected override void OnDie()
    {
        Debug.Log("�������� ����߽��ϴ�.");
    }
}