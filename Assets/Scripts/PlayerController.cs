using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int hp = 60;

    public void UseCard(int damage)
    {
        Debug.Log("플레이어가 카드를 사용하여 " + damage + "의 데미지를 가했습니다.");
        // 실제로는 카드를 사용하고, 해당 카드의 효과를 구현하는 로직이 필요합니다.
    }

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