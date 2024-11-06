using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public PlayerController player;
    public EnemyController enemy;

    private bool isPlayerTurn = true;
    private int cardsPlayedThisTurn = 0;
    private const int maxCardsPerTurn = 2;
    private List<Card> playerHand = new List<Card>();

    void Start()
    {
        StartBattle();
    }

    void Update()
    {
        if (isPlayerTurn)
        {
            if (Input.GetKeyDown(KeyCode.Space) && cardsPlayedThisTurn < maxCardsPerTurn)
            {
                // 예시로 스페이스 키를 누르면 카드를 사용함
                if (playerHand.Count > 0)
                {
                    PlayCard(playerHand[0]);
                }
            }
        }
    }

    void StartBattle()
    {
        Debug.Log("전투 시작!");
        PlayerTurn();
    }

    void PlayerTurn()
    {
        isPlayerTurn = true;
        cardsPlayedThisTurn = 0;
        DrawCards(5);
        Debug.Log("플레이어의 턴입니다. 카드를 사용하세요.");
    }

    void EnemyTurn()
    {
        isPlayerTurn = false;
        Debug.Log("적의 턴입니다.");
        enemy.AttackPlayer();
        EndTurn();
    }

    public void PlayCard(Card card)
    {
        if (isPlayerTurn && cardsPlayedThisTurn < maxCardsPerTurn)
        {
            card.Use();
            cardsPlayedThisTurn++;
            Debug.Log("카드를 사용했습니다. 이번 턴에 사용한 카드 수: " + cardsPlayedThisTurn);
        }
        else if (!isPlayerTurn)
        {
            Debug.Log("지금은 플레이어의 턴이 아닙니다.");
        }
        else
        {
            Debug.Log("이번 턴에는 더 이상 카드를 사용할 수 없습니다.");
        }
    }

    void DrawCards(int numberOfCards)
    {
        for (int i = 0; i < numberOfCards; i++)
        {
            Card newCard = new Card();  // 임시로 카드 객체를 생성 (실제 카드 생성 로직 필요)
            playerHand.Add(newCard);
            Debug.Log("카드를 드로우했습니다.");
        }
    }

    void DiscardHand()
    {
        playerHand.Clear();
        Debug.Log("플레이어의 손에 있는 모든 카드를 버렸습니다.");
    }

    public void EndTurn()
    {
        if (isPlayerTurn)
        {
            DiscardHand();
            EnemyTurn();
        }
        else
        {
            PlayerTurn();
        }
    }
}


public class Card
{
    public void Use()
    {
        Debug.Log("카드가 사용되었습니다.");
        // 카드 효과 구현 필요
    }
}
