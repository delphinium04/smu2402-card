using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    private int hp = 60;

    private void Awake()
    {
        // 싱글턴 인스턴스 설정
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // 씬 전환 시에도 유지되도록 설정
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
