using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private int hp = 12;

    public void AttackPlayer()
    {
        int damage = 10;  // 정해진 공격력
        Debug.Log("적이 플레이어를 공격합니다. 데미지: " + damage);
        FindObjectOfType<PlayerController>().TakeDamage(damage);
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        Debug.Log("적이 " + damage + "의 피해를 입었습니다. 남은 체력: " + hp);
        if (hp <= 0)
        {
            OnDie();
        }
    }

    private void OnDie()
    {
        Debug.Log("적이 사망했습니다.");
    }
}