using UnityEngine;

[CreateAssetMenu(fileName = "PirateLieutenant", menuName = "Enemy/PirateLieutenant")]
public class PirateLieutenant : BaseEnemy
{
    private bool IsHeal = false;
    public override void ActivatePattern(EnemyBehaviour enemy)
    {
        PlayerController.Instance.TakeDamage(Damage);

        if (Random.value <= 0.5f) // 50% Ȯ��
        {
            PlayerController.Instance.TakeMultiplier += 50;
        }

        if (enemy.HP <= 30 && !IsHeal)
        {
            enemy.Heal(30);
            IsHeal = true;
        }
    }
}