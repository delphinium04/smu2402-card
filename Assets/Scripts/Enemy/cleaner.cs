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
        Debug.Log("û�Һΰ� �÷��̾ �����մϴ�. ������: " + damage);
        FindObjectOfType<PlayerController>().TakeDamage(damage);
    }

    protected override void OnDie()
    {
        Debug.Log("û�Һΰ� ����߽��ϴ�.");
    }
}