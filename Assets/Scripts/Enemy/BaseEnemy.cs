using System;
using Entity;
using UnityEngine;

namespace Enemy
{
    public enum Type
    {
        Boatswain, Cleaner, CombatSailor, PirateCaptain, PirateLieutenant, PlunderingCrew, SeaCrow, WatchfulSallor
    }
    
    [RequireComponent(typeof(SpriteRenderer))]
    public class BaseEnemy: AbstractEntity
    {
        public Action<BaseEnemy> OnDeath;
        public Action<BaseEnemy> OnClicked;

        public int Weight { get; protected set; }
        public override void Init(EntityData _data)
        {
            base.Init(_data);
            Weight = _data.Weight;
        }

        void Awake()
        {
            if(data == null) return;
            Init(data);
        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            
            if(Hp <= 0) OnDeath?.Invoke(this);
        }

        public virtual void ActivatePattern()
        {
        }

        private void OnMouseDown()
        {
            OnClicked?.Invoke(this);
        }
    }
}