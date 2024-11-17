using UnityEngine;

[CreateAssetMenu(fileName = "SeaCrow", menuName = "Enemy/SeaCrow")]
public class SeaCrow : BaseEnemy
{
    public override void ActivatePattern(EnemyBehaviour enemy)
    {
        PlayerController.Instance.TakeDamage(Damage);
    }
}