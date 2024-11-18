using System;
using UnityEngine;

namespace Entity
{
    /*
     * 모든 개체(플레이어, NPC 등) 의 기본입니다.
     * EntityData를 기본으로 스탯 초기화를 하며
     * Atk, HP, 힐과 데미지 입는 메소드 등이 구현되어 있습니다.
     * Atk를 가져올 시 자동으로 배율이 곱해진 값을 전달합니다.
     */
    public abstract class AbstractEntity : MonoBehaviour
    {
        protected EntityData data;
        protected Sprite image;
        public string Name { get; protected set; }
        public int Hp { get; protected set; }

        int atk;

        public int Atk
        {
            get => (int)(atk * (atkMultiplier / 100.0f));
            protected set => atk = value;
        }

        protected int atkMultiplier = 100;

        public int AtkMultiplier
        {
            get => atkMultiplier;
            set
            {
                atkMultiplier = value;
                Debug.Log("플레이어의 공격 효과 계수가 " + atkMultiplier + "%로 설정되었습니다.");
            }
        }

        protected int healMultiplier = 100;

        public int HealMultiplier
        {
            get => healMultiplier;
            set
            {
                healMultiplier = value;
                Debug.Log("플레이어의 공격 효과 계수가 " + healMultiplier + "%로 설정되었습니다.");
            }
        }

        protected int takeMultiplier = 100;

        public int TakeMultiplier
        {
            get => takeMultiplier;
            set
            {
                takeMultiplier = value;
                Debug.Log("플레이어의 공격 효과 계수가 " + takeMultiplier + "%로 설정되었습니다.");
            }
        }

        public virtual void Init(EntityData _data)
        {
            data = _data;
            Hp = _data.Hp;
            Atk = _data.Atk;
            image = _data.Sprite;
            Name = _data.Name;
            
            GetComponent<SpriteRenderer>().sprite = image;
        }

        public virtual void TakeDamage(int damage)
        {
            int value = (int)(damage * TakeMultiplier / 100.0f);
            Hp -= value;
            Debug.Log($"{data.name}: 피해 {value} 입음");
        }

        public virtual void Heal(int heal)
        {
            int value = (int)(heal * HealMultiplier / 100.0f);
            Hp += heal;
            Debug.Log($"{data.name}: 체력 {heal} 회복");
        }
    }
}