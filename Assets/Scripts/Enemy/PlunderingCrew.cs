using UnityEngine;

[CreateAssetMenu(fileName = "PlunderingCrew", menuName = "Enemy/PlunderingCrew")]
public class PlunderingCrew : BaseEnemy
{
    public override void ActivatePattern(EnemyBehaviour enemy)
    {
        PlayerController.Instance.TakeDamage(Damage);

        PlayerController.Instance.DecreaseGold(10);
    }
}