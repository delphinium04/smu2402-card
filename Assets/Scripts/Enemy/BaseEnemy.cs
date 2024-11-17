using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "Enemy/BaseEnemy")]
public class BaseEnemy : ScriptableObject
{
    [SerializeField] private string enemyName;
    public string EnemyName => enemyName;

    [SerializeField] private int maxHp;
    public int MaxHp => maxHp;

    [SerializeField] private int damage;
    public int Damage => damage;

    [SerializeField] private int weight;
    public int Weight => weight;

    [SerializeField] private Sprite enemyImage;
    public Sprite EnemyImage => enemyImage;

    [SerializeField][TextArea] private string description;
    public string Description => description;

    public virtual void ActivatePattern(EnemyBehaviour enemy)
    {
        return;
    }
}
