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
        Debug.Log("������ ������ �÷��̾ �����մϴ�. ������: " + damage);
        FindObjectOfType<PlayerController>().TakeDamage(damage);
    }

    protected override void OnDie()
    {
        Debug.Log("�����ϴ� ������ ����߽��ϴ�.");
    }
}