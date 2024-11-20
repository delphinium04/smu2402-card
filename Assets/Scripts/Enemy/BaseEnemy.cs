using System;
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

    [RequireComponent(typeof(SpriteRenderer))]
    public class BaseEnemy : MonoBehaviour
    {
        public Action<int, int> OnHpChanged = null;

        protected EnemyData data;

        Sprite image;
        EntityUI _ui;

        public string Name { get; set; }

        int _maxHp;
        int _hp;
        public int Hp {
            get => _hp;
            set
            {
                _hp = value;
                OnHpChanged?.Invoke(_hp, _maxHp);
            }
            }

        int atk;

        public int Atk
        {
            get => (int)(atk * (_atkMultiplier / 100.0f));
            private set => atk = value;
        }

        int _atkMultiplier = 100;
        public int AtkMultiplier
        {
            get => _atkMultiplier;
            set
            {
                _atkMultiplier = value;
                Debug.Log("플레이어의 공격 효과 계수가 " + _atkMultiplier + "%로 설정되었습니다.");
            }
        }

        int _healMultiplier = 100;
        public int HealMultiplier
        {
            get => _healMultiplier;
            set
            {
                _healMultiplier = value;
                Debug.Log("플레이어의 공격 효과 계수가 " + _healMultiplier + "%로 설정되었습니다.");
            }
        }

        int _takeMultiplier = 100;
        public int TakeMultiplier
        {
            get => _takeMultiplier;
            set
            {
                _takeMultiplier = value;
                Debug.Log("플레이어의 공격 효과 계수가 " + _takeMultiplier + "%로 설정되었습니다.");
            }
        }

        void Awake()
        {
            _ui = GetComponentInChildren<EntityUI>();
            
            if (data == null) return;
            Init(data);
            }

        void Start()
        {
            _ui.UpdateHP(Hp, _maxHp);
            OnHpChanged += _ui.UpdateHP;
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
            image = _data.Sprite;
            Name = _data.Name;

            GetComponent<SpriteRenderer>().sprite = image;
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

        public Action<BaseEnemy> OnDeath;

        public Action<BaseEnemy> OnClicked;

        public int Weight { get; set; }

        public virtual void ActivatePattern()
        {
        }
    }
}