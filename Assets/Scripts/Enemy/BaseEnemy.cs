using System;
using Effect;
using UnityEngine;

namespace Enemy
{
    public enum Type
    {
        Boatswain,
        Cleaner,
        CombatSailor,
        PirateCaptain,
        PirateLieutenant,
        PlunderingCrew,
        SeaCrow,
        WatchfulSallor
    }

    [RequireComponent(typeof(SpriteRenderer), typeof(EffectManager))]
    public class BaseEnemy : MonoBehaviour
    {
        protected EnemyData data;

        public Action<int, int> OnHpChanged = null;
        public Action<BaseEnemy> OnDeath = null;
        public Action<BaseEnemy> OnClicked = null;

        public int Weight { get; set; }

        public string Name { get; set; }

        int _maxHp;
        int _hp;

        protected int Hp
        {
            get => _hp;
            set
            {
                _hp = value;
                OnHpChanged?.Invoke(_hp, _maxHp);
            }
        }


        int _atk;

        protected int Atk
        {
            get => (int)(_atk * (_atkMultiplier / 100.0f));
            private set => _atk = value;
        }

        int _atkMultiplier = 100;

        public int AtkMultiplier
        {
            get => _atkMultiplier;
            set
            {
                _atkMultiplier = value;
                Debug.Log($"{Name}의 공격 효과 계수 => " + AtkMultiplier + "%");
            }
        }

        int _healMultiplier = 100;

        public int HealMultiplier
        {
            get => _healMultiplier;
            set
            {
                _healMultiplier = value;
                Debug.Log($"{Name}의 회복 효과 계수 => " + HealMultiplier + "%");
            }
        }

        int _takeMultiplier = 100;

        public int TakeMultiplier
        {
            get => _takeMultiplier;
            set
            {
                _takeMultiplier = value;
                Debug.Log($"{Name}의 받는 피해 계수 => " + TakeMultiplier + "%");
            }
        }

        void Awake()
        {
            if (data == null) return;
            Init(data);
        }

        void Start()
        {
            OnHpChanged?.Invoke(_hp, _maxHp);

        }

        private void OnMouseDown()
        {
            OnClicked?.Invoke(this);
        }

        public void Init(EnemyData _data)
        {
            data = _data;
            Hp = _data.Hp;
            _maxHp = _data.Hp;
            Atk = _data.Atk;
            Name = _data.Name;

            GetComponent<SpriteRenderer>().sprite = _data.Sprite;
            OnHpChanged?.Invoke(Hp, _maxHp);
        }

        public void TakeDamage(int damage)
        {
            int value = (int)(damage * TakeMultiplier / 100.0f);
            Hp -= value;
            Debug.Log($"{data.Name}: {value} 데미지 입음");

            if (Hp <= 0) OnDeath?.Invoke(this);
        }

        public void Heal(int heal)
        {
            int value = (int)(heal * HealMultiplier / 100.0f);
            Hp += value;
            Debug.Log($"{data.Name}: 체력 {value} 회복");
        }


        public virtual void ActivatePattern()
        {
        }
    }
}