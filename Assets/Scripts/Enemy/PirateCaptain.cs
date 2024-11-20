using Enemy;
using UnityEngine;

namespace Enemy
{
    public class PirateCaptain : BaseEnemy
    {
        private int turnCount = 0;

        private void ApplyDebuffToPlayer()
        {
            float chance = Random.Range(0f, 1f);
            if (chance <= 0.25f) // 25% 확률로 디버프 부여
            {
                int debuffType = Random.Range(0, 3); // 0: 약화, 1: 침묵, 2: 속박

                switch (debuffType)
                {
                    case 0:
                        PlayerController.Instance.AtkMultiplier -= 25;
                        Debug.Log("플레이어에게 약화 디버프를 부여했습니다. 주는 데미지 25% 감소");
                        break;
                    case 1:
                        PlayerController.Instance.canSelectSkillCard = false;
                        PlayerController.Instance.canSelectSpecialCard = false;
                        Debug.Log("플레이어에게 침묵 디버프를 부여했습니다. 스킬카드 및 특수카드 사용 불가");
                        break;
                    case 2:
                        PlayerController.Instance.canSelectNormalCard = false;
                        Debug.Log("플레이어에게 속박 디버프를 부여했습니다. 일반 카드 사용 불가");
                        break;
                }
            }
        }

        private void RemoveRandomDebuff()
        {
            // 디버프가 있는지 확인하고, 랜덤하게 하나를 제거
            if (HasDebuff())
            {
                int debuffToRemove = Random.Range(0, GetDebuffCount());
                RemoveDebuffByIndex(debuffToRemove);
                Debug.Log("자신에게 걸린 디버프 중 하나를 해제했습니다.");
            }
        }

        private bool HasDebuff()
        {
            // 적이 걸린 디버프가 있는지 여부를 확인하는 함수
            // 로직 구현 필요 (현재 디버프 상태들을 관리하는 방식에 따라 달라짐)
            return true; // 예시로 항상 디버프가 있다고 가정
        }

        private int GetDebuffCount()
        {
            // 현재 걸려 있는 디버프의 개수를 반환하는 함수
            // 이 로직 구현 필요
            return 3; // 예시로 3개의 디버프가 있다고 가정
        }

        private void RemoveDebuffByIndex(int index)
        {
            // 특정 인덱스의 디버프를 해제하는 함수
            // 이 로직 구현 필요 (디버프 구현 완료되면 구현 가능)ㄴ
        }

        public override void ActivatePattern()
        {
            turnCount++;

            // 2회 공격
            for (int i = 0; i < 2; i++)
            {
                PlayerController.Instance.TakeDamage(Atk);
                // 각 공격마다 25% 확률로 디버프 부여
                ApplyDebuffToPlayer();
            }

            // 2턴마다 자신에게 걸린 디버프 1개 해제
            if (turnCount % 2 == 0)
            {
                RemoveRandomDebuff();
            }
        }
    }
}
