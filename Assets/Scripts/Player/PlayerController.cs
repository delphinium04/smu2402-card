using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int hp = 60;

    public void TakeDamage(int damage)
    {
        hp -= damage;
        Debug.Log("�÷��̾ " + damage + "�� ���ظ� �Ծ����ϴ�. ���� ü��: " + hp);
        if (hp <= 0)
        {
            OnDie();
        }
    }

    private void OnDie()
    {
        Debug.Log("�÷��̾ ����߽��ϴ�.");
    }
}