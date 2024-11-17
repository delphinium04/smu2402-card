using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private BaseEnemy enemyData; // �� ������Ʈ ������ ����

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
        Debug.Log($"{enemyData.EnemyName}��(��) {damage}�� ���ظ� �Ծ����ϴ�. ���� ü��: {hp}");
    }

    public void Heal(int heal)
    {
        hp += heal;
        Debug.Log($"{enemyData.EnemyName}��(��) {heal}��ŭ ä���� ȸ���մϴ�. ���� ü��: {hp}");
    }

    public bool IsDead()
    {
        if (hp <= 0)
        {
            Debug.Log($"{enemyData.EnemyName}��(��) ����߽��ϴ�.");
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