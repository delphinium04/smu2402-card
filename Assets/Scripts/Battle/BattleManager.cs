using System.Collections.Generic;
using UnityEngine;
using Card;

/// <summary>
/// 카드 턴제 게임이므로 Update()함수 구현이 아직은 필요하지 않지만 카드 애니메이션을 추가하게 된다면 구현 필요
/// CardManager에 카드의 사용 이벤트 함수와 사용 또는 버려진 카드 덱 구현 필요
/// 턴 종료 UI와 스테이지 진행(맵) 등 이벤트 구현 필요
/// </summary>
public class BattleManager : MonoBehaviour
{
    public PlayerController player;
    public BaseEnemy currentEnemy;
    public CardManager cardManager;  // CardManager 인스턴스 참조

    private int maxHandSize = 5;
    private int cardsPlayedThisTurn = 0;
    private int maxCardsPerTurn = 2;

    private bool isPlayerTurn = true;
    private List<CardBehaviour> hand = new List<CardBehaviour>();
    private List<CardBehaviour> discard = new List<CardBehaviour>();

    void Start()
    {
        InitializePlayerDeck(); // 전투 시작 시 덱 초기화
        StartBattle();          // 전투 시작
    }

    void InitializePlayerDeck()
    {
        // 기본 덱에 들어갈 카드 목록과 개수 설정 필요
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

        // 카드 드로우
        hand = new List<CardBehaviour>(cardManager.DrawCard(maxHandSize));
        Debug.Log("플레이어의 턴입니다. 카드를 사용하세요.");
    }

    // CardManager에 PlayCard를 실행하기 위한 이벤트 함수 구현 필요
    public void PlayCard(CardBehaviour card, GameObject target)
    {
        if (!isPlayerTurn || cardsPlayedThisTurn >= maxCardsPerTurn)
        {
            Debug.Log("카드를 더 이상 사용할 수 없습니다.");
            return;
        }

        card.Use(target);
        hand.Remove(card);
        // 버려진 카드 관리 - CardManager에 DiscardDeck 구현 필요
        // cardManager.DiscardCard(card);
        cardsPlayedThisTurn++;

        if (cardsPlayedThisTurn >= maxCardsPerTurn)
        {
            Debug.Log("최대 카드 사용 수에 도달했습니다.");
        }
    }

    // 턴 종료 버튼 UI필요 (이벤트 함수 구현 필요)
    public void EndTurn()
    {
        if (isPlayerTurn)
        {
            Debug.Log("플레이어의 턴이 종료되었습니다. 사용하지 않은 카드는 모두 버립니다.");
            // 남은 카드를 discardPile로 이동 - CardManager에 DiscardDeck 구현 필요
            // cardManager.DiscardHand(hand);
            hand.Clear();
            EnemyTurn();
        }
        else
        {
            PlayerTurn();
        }
    }

    void EnemyTurn()
    {
        isPlayerTurn = false;
        Debug.Log("적의 턴입니다.");
        currentEnemy.AttackPlayer();
        EndTurn();
    }
}
