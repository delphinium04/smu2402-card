using UnityEngine;

[CreateAssetMenu(fileName = "cleaner", menuName = "Enemy/cleaner")]
public class cleaner : BaseEnemy
{
    public override void ActivatePattern(EnemyBehaviour enemy)
    {
        PlayerController.Instance.TakeDamage(Damage);
    }
}  