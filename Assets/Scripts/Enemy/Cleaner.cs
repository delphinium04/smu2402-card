using UnityEngine;

[CreateAssetMenu(fileName = "Cleaner", menuName = "Enemy/Cleaner")]
public class Cleaner : BaseEnemy
{
    public override void ActivatePattern(EnemyBehaviour enemy)
    {
        PlayerController.Instance.TakeDamage(Damage);
    }
}  