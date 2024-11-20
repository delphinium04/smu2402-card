using Enemy;
using UnityEngine;

namespace Enemy
{
    public class PirateLieutenant : BaseEnemy
    {
        private bool _isHeal = false;
 
        public override void ActivatePattern()
        {
            PlayerController.Instance.TakeDamage(Atk);

            if (Random.value <= 0.5f)
            {
                // PlayerController.Instance.TakeMultiplier += 50;
                // 취약 부여
            }

            if (Hp > 30 || _isHeal) return;
            Heal(30);
            _isHeal = true;
        }
    }
}