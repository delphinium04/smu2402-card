using UnityEngine;

[CreateAssetMenu(fileName = "PirateLieutenant", menuName = "Enemy/PirateLieutenant")]
public class PirateLieutenant : BaseEnemy
{
    private bool IsHeal = false;
    public override void ActivatePattern(EnemyBehaviour enemy)
    {
        PlayerController.Instance.TakeDamage(Damage);

        if (Random.value <= 0.5f) // 50% È®·ü
        {
            PlayerController.Instance.SetTakeEffectMultiplier(150);
        }

        if (enemy.GetHp() <= 30 && !IsHeal)
        {
            enemy.Heal(30);
            IsHeal = true;
        }
    }
}