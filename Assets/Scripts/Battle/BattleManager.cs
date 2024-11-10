using System.Collections.Generic;
using UnityEngine;
using Card;

/// <summary>
/// ī�� ���� �����̹Ƿ� Update()�Լ� ������ ������ �ʿ����� ������ ī�� �ִϸ��̼��� �߰��ϰ� �ȴٸ� ���� �ʿ�
/// CardManager�� ī���� ��� �̺�Ʈ �Լ��� ��� �Ǵ� ������ ī�� �� ���� �ʿ�
/// �� ���� UI�� �������� ����(��) �� �̺�Ʈ ���� �ʿ�
/// </summary>
public class BattleManager : MonoBehaviour
{
    public PlayerController player;
    public BaseEnemy currentEnemy;
    public CardManager cardManager;  // CardManager �ν��Ͻ� ����

    private int maxHandSize = 5;
    private int cardsPlayedThisTurn = 0;
    private int maxCardsPerTurn = 2;

    private bool isPlayerTurn = true;
    private List<CardBehaviour> hand = new List<CardBehaviour>();

    void Start()
    {
        InitializePlayerDeck(); // ���� ���� �� �� �ʱ�ȭ
        StartBattle();          // ���� ����
    }

    void InitializePlayerDeck()
    {
        // �⺻ ���� �� ī�� ��ϰ� ���� ���� �ʿ�
    }

    void StartBattle()
    {
        Debug.Log("���� ����!");
        PlayerTurn();
    }

    void PlayerTurn()
    {
        isPlayerTurn = true;
        cardsPlayedThisTurn = 0;

        // ī�� ��ο�
        hand = new List<CardBehaviour>(cardManager.DrawCard(maxHandSize));
        Debug.Log("�÷��̾��� ���Դϴ�. ī�带 ����ϼ���.");
    }

    // CardManager�� PlayCard�� �����ϱ� ���� �̺�Ʈ �Լ� ���� �ʿ�
    public void PlayCard(CardBehaviour card, GameObject target)
    {
        if (!isPlayerTurn || cardsPlayedThisTurn >= maxCardsPerTurn)
        {
            Debug.Log("ī�带 �� �̻� ����� �� �����ϴ�.");
            return;
        }

        card.Use(target);
        hand.Remove(card);
        // ������ ī�� ���� - CardManager�� DiscardDeck ���� �ʿ�
        // cardManager.DiscardCard(card);
        cardsPlayedThisTurn++;

        if (cardsPlayedThisTurn >= maxCardsPerTurn)
        {
            Debug.Log("�ִ� ī�� ��� ���� �����߽��ϴ�.");
        }
    }

    // �� ���� ��ư UI�ʿ� (�̺�Ʈ �Լ� ���� �ʿ�)
    public void EndTurn()
    {
        if (isPlayerTurn)
        {
            Debug.Log("�÷��̾��� ���� ����Ǿ����ϴ�. ������� ���� ī��� ��� �����ϴ�.");
            // ���� ī�带 discardPile�� �̵� - CardManager�� DiscardDeck ���� �ʿ�
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
        Debug.Log("���� ���Դϴ�.");
        currentEnemy.AttackPlayer();
        EndTurn();
    }
}