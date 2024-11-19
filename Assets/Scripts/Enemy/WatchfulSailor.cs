using Entity;
using UnityEngine;

namespace Enemy
{
    public class WatchfulSailor : BaseEnemy
    {
        public override void ActivatePattern()
        {
            PlayerController.Instance.TakeDamage(Atk);
            PlayerController.Instance.TakeDamage(Atk);

        }
    }
}