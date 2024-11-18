using Entity;
using UnityEngine;

namespace Enemy
{
    public class SeaCrow : BaseEnemy
    {
        public override void ActivatePattern()
        {        
            PlayerController.Instance.TakeDamage(Atk);
        }
    }
}