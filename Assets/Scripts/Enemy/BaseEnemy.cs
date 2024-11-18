using System;
using Entity;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class BaseEnemy: AbstractEntity
    {
        public Action<BaseEnemy> OnDeath;

        public int Weight { get; protected set; }
        public override void Init(EntityData _data)
        {
            base.Init(_data);
            Weight = _data.Weight;
        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            
            if(Hp <= 0) OnDeath?.Invoke(this);
        }

        public virtual extern void ActivatePattern();
    }
}