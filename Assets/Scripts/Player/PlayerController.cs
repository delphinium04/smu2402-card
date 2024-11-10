using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int hp = 60;

    public void TakeDamage(int damage)
    {
        hp -= damage;
        Debug.Log("플레이어가 " + damage + "의 피해를 입었습니다. 남은 체력: " + hp);
        if (hp <= 0)
        {
            OnDie();
        }
    }

    private void OnDie()
    {
        Debug.Log("플레이어가 사망했습니다.");
    }
}