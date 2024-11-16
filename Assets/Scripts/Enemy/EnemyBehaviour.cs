using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private BaseEnemy enemyData;

    private int hp;

    private string enemyName;
    public string EnemyName => enemyName;

    private void Awake()
    {
        hp = enemyData.MaxHp;
        enemyName = enemyData.EnemyName;
    }

    public void AttackPlayer()
    {
        Debug.Log($"{enemyData.EnemyName}이(가) 플레이어를 공격합니다. 데미지: {enemyData.Damage}");
        PlayerController.Instance.TakeDamage(enemyData.Damage);
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        Debug.Log($"{enemyData.EnemyName}이(가) {damage}의 피해를 입었습니다. 남은 체력: {hp}");
    }

    public bool IsDead()
    {
        Debug.Log($"{enemyData.EnemyName}이(가) 사망했습니다.");
        Destroy(gameObject);
        return true;
    }
}
