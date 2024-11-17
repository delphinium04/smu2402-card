using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Boatswain", menuName = "Enemy/Boatswain")]
public class Boatswain : BaseEnemy
{
    [SerializeField]
    private GameObject cleanerPrefab; // 청소부 프리팹을 참조하는 변수

    private bool IsCleaner = false;

    public override void ActivatePattern(EnemyBehaviour enemy)
    {
        Debug.Log($"Boatswain이(가) 2회 공격합니다. 남은 체력:" + PlayerController.Instance.Hp);
        for (int i = 0; i < 2; i++)
        {
            PlayerController.Instance.TakeDamage(Damage);
        }

        if (enemy.HP <= 30 && !IsCleaner)
        {
            SummonCleaner(); // 체력이 30 이하로 내려갈 경우 특수 행동
            IsCleaner = true;
        }
    }

    public void SummonCleaner()
    {
        Debug.Log($"{EnemyName}이 청소부를 소환합니다.");

        // 프리팹을 소환하기 위한 위치 설정 (해당 적의 동적 생성 구현 후 다시 구현하기)
        Vector2 spawnPosition = new Vector2(1, 0); // 현재 위치 기준으로 약간 오른쪽에 생성 ( + transform.position)

        Instantiate(cleanerPrefab, spawnPosition, Quaternion.identity);
    }
}