using UnityEngine;

[CreateAssetMenu(fileName = "WatchfulSailor", menuName = "Enemy/WatchfulSailor")]
public class WatchfulSailor : BaseEnemy
{
    public override void ActivatePattern(EnemyBehaviour enemy)
    {
        for (int i = 0; i < 2; i++)
        {
            PlayerController.Instance.TakeDamage(Damage);
        }
    }
}