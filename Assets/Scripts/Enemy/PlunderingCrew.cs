using Enemy;
using UnityEngine;

namespace Enemy
{
    public class PlunderingCrew : BaseEnemy
    {
        public override void ActivatePattern()
        {
            PlayerController.Instance.TakeDamage(Atk);
        }
    }
}