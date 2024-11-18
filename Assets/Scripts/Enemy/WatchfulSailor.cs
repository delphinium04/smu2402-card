using Entity;
using UnityEngine;

namespace Enemy
{
    public class WatchfulSailor : BaseEnemy
    {
        public override void ActivatePattern()
        {
            for (int i = 0; i < 2; i++)
            {
                PlayerController.Instance.TakeDamage(Atk);
            }
        }
    }
}