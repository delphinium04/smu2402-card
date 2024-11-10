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
        Debug.Log("��Ż ������ �÷��̾ �����մϴ�. ������: " + damage);
        FindObjectOfType<PlayerController>().TakeDamage(damage);
    }

    protected override void OnDie()
    {
        Debug.Log("��Ż �¹����� ����߽��ϴ�.");
    }
}