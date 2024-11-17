using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private BaseEnemy enemyData; // 적 오브젝트 데이터 참조

    private int hp;

    private string enemyName;
    public string EnemyName => enemyName;

    private void Awake()
    {
        hp = enemyData.MaxHp;
        enemyName = enemyData.EnemyName;
    }

    public int GetHp()
    {
        return hp;
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        Debug.Log($"{enemyData.EnemyName}이(가) {damage}의 피해를 입었습니다. 남은 체력: {hp}");
    }

    public void Heal(int heal)
    {
        hp += heal;
        Debug.Log($"{enemyData.EnemyName}이(가) {heal}만큼 채력을 회복합니다. 남은 체력: {hp}");
    }

    public bool IsDead()
    {
        if (hp <= 0)
        {
            Debug.Log($"{enemyData.EnemyName}이(가) 사망했습니다.");
            Destroy(gameObject);
            return true;
        }
        else return false;
    }

    public void Playpattern(EnemyBehaviour enemy)
    {
        enemyData.ActivatePattern(enemy);
    }
}
