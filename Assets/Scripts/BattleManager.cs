using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public GameObject enemy;
    private PlayerController playerController;
    private EmemyController enemyController;
    private int card = 5;

    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        enemyController = enemy.GetComponent<EmemyController>();

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("전투가 시작됩니다.");
    }

    // 적이 공격했을 때
    public void EnemyAttack()
    {
        if (playerController != null)
        {
            playerController.TakeDamage(enemyController.GetDamage());
        }
    }

    // 플레이어 카드 개수 반환
    public int GetCardNumber(int card)
    {
        return card;
    }

    // 턴 종료시 카드 버림
    public void TurnEnd(int card)
    {
        if (card > 0) card = 0;
    }

    // 플레이어 턴에 카드 드로우
    public void TurnDraw(int card)
    {
        card = 5;
    }
}
