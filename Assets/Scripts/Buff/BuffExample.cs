using UnityEngine;

namespace Buff
{
    public class BuffExample : BaseEffect
    {
        // 예시: 2턴간 공격력 3 증가
        private int _amount;

        public BuffExample(GameObject t, int duration, int amount) : base(t, duration, Type.Example)
        {
            _amount = amount;
        }

        public override void ApplyEffect()
        {
            Debug.Log($"{Type.Example.ToString()} Buff: Increase {Target.name}'s Attack {_amount}");
            // Player.Attack += _amount;
            // GameManager.TurnIncreased(Event) += TurnIncreased;
        }

        public override void RemoveEffect()
        {
            Debug.Log($"{Target.name}-{Type.Example.ToString()} End");
            // Player.Attack -= _amount;
        }

        public void TurnIncreased()
        {
            TurnDuration--;
            if (TurnDuration == 0)
            {
                RemoveEffect();
            }
        }
    }
}
