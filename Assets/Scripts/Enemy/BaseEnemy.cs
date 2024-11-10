using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    protected int hp;
    protected int damage;
    protected int weight;

    public virtual void AttackPlayer()
    {
        Debug.Log("���� �÷��̾ �����մϴ�. ������: " + damage);
        FindObjectOfType<PlayerController>().TakeDamage(damage);
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        Debug.Log("���� " + damage + "�� ���ظ� �Ծ����ϴ�. ���� ü��: " + hp);
        if (hp <= 0)
        {
            OnDie();
        }
    }

    protected virtual void OnDie()
    {
        Debug.Log("���� ����߽��ϴ�.");
    }
}