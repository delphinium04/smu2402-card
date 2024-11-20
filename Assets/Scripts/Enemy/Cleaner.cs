using Enemy;
using UnityEngine;

namespace Enemy
{
    public class Cleaner : BaseEnemy
    {
        public override void ActivatePattern()
        {
            PlayerController.Instance.TakeDamage(Atk);
        }
    }
}  