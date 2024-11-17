using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private BaseEnemy enemyData; // �� ������Ʈ ������ ����

    public int HP { get; private set; }

    private string enemyName;
    public string EnemyName => enemyName;

    // 공격, 피해, 힐에 곱하는 계수 (기본값 100%)
    private int attackMultiplier = 100;

    public int AttackMultiplier
    {
        get => attackMultiplier;
        set
        {
            attackMultiplier = value;
            Debug.Log("플레이어의 공격 효과 계수가 " + attackMultiplier + "%로 설정되었습니다.");
        }
    }

    private int healMultiplier = 100;

    public int HealMultiplier
    {
        get => healMultiplier;
        set
        {
            healMultiplier = value;
            Debug.Log("플레이어의 공격 효과 계수가 " + healMultiplier + "%로 설정되었습니다.");
        }
    }

    private int takeMultiplier = 100;

    public int TakeMultiplier
    {
        get => takeMultiplier;
        set
        {
            takeMultiplier = value;
            Debug.Log("플레이어의 공격 효과 계수가 " + takeMultiplier + "%로 설정되었습니다.");
        }
    }

    private void Awake()
    {
        HP = enemyData.MaxHp;
        enemyName = enemyData.EnemyName;
    }

    public void TakeDamage(int damage)
    {
        HP -= damage;
        Debug.Log($"{enemyData.EnemyName} {damage} 피해 입음, 남은 HP: {HP}");
    }

    public void Heal(int heal)
    {
        HP += heal;
        Debug.Log($"{enemyData.EnemyName} {heal} 회복, 남은 HP: {HP}");
    }

    public bool IsDead()
        => HP <= 0;

    public void PlayPattern(EnemyBehaviour enemy)
    {
        enemyData.ActivatePattern(enemy);
    }
}