using Entity;
using UnityEngine;

namespace Enemy
{
    public class PlunderingCrew : BaseEnemy
    {
        public override void ActivatePattern()
        {
            PlayerController.Instance.TakeDamage(Atk);
            PlayerController.Instance.DecreaseGold(10);
        }
    }
}