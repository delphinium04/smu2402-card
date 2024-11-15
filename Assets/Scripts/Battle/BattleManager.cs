using System.Collections.Generic;
using UnityEngine;
using Card;

/// <summary>
/// 전투 시스템을 관리하는 싱글턴 클래스입니다. 전투의 흐름과 턴 진행을 제어하며, 플레이어와 적 간의 상호작용을 담당합니다.
/// 카드매니저 스크립트를 싱글턴으로 구현했습니다. (플레이어의 덱을 저장함으로써 원본의 덱을 통해 전투를 진행하기 위함)
/// 카드 강화 시스템 미구현
/// 전투 종료 함수 미구현
/// </summary>

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }

    public List<BaseEnemy> Enemy = new List<BaseEnemy>();

    private int maxHandSize = 5;
    private int cardsPlayedThisTurn = 0;
    private int maxCardsPerTurn = 3;

    private bool isPlayerTurn = true;
    public bool IsPlayerTurn => isPlayerTurn;
    
    private List<CardBehaviour> hand = new List<CardBehaviour>();
    private List<CardBehaviour> discardDeck = new List<CardBehaviour>();
    private List<CardBehaviour> useList = new List<CardBehaviour>();

    private BaseEnemy selectedEnemy;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Startbattle();
    }

    private void Startbattle()
    {
        
        PlayerTurn();
    }

    private void PlayerTurn()
    {
        isPlayerTurn = true;
        cardsPlayedThisTurn = 0;
        hand = new List<CardBehaviour>(CardManager.Instance.DrawCard(maxHandSize));
        useList.Clear();
        Debug.Log("플레이어의 턴입니다. 카드를 사용하세요.");
    }

    // 적 오브젝트 타겟팅
    public void SelectEnemy(BaseEnemy enemy)
    {
        if (!isPlayerTurn)
        {
            Debug.LogWarning("현재 플레이어의 턴이 아닙니다.");
            return;
        }

        selectedEnemy = enemy;
        Debug.Log("적이 선택되었습니다: " + enemy.gameObject.name);
    }

    // 이번턴에 사용할 카드(핸드 카드 오브젝트들 클릭 또는 드래그로 이벤트 함수로 호출)
    public void SelectCardForUse(CardBehaviour card)
    {
        if (!isPlayerTurn)
        {
            Debug.LogWarning("현재 플레이어의 턴이 아닙니다.");
            return;
        }

        if (useList.Count >= maxCardsPerTurn)
        {
            Debug.Log("이번 턴에 사용할 수 있는 최대 카드 수에 도달했습니다.");
            return;
        }

        if (hand.Contains(card))
        {
            useList.Add(card);
            hand.Remove(card);
            Debug.Log(card.Card.CardName + " 카드를 이번 턴에 사용하도록 선택했습니다.");

            // 단일 타겟 카드인 경우 적 선택 유도
            if (card.Card.TargetingType == TargetingType.Single)
            {
                Debug.Log("단일 타겟 카드가 선택되었습니다. 적을 선택하세요.");
            }
        }
    }

    // 이번 턴에 선택된 카드를 취소하고 핸드로 되돌림
    public void CancelCardSelection(CardBehaviour card)
    {
        if (useList.Contains(card))
        {
            useList.Remove(card);
            hand.Add(card);
            Debug.Log(card.Card.CardName + " 카드 선택이 취소되었습니다.");
        }
    }

    // 턴 종료 버튼 UI 이벤트로 호출(플레이어 턴 종료하면서 카드 사용)
    public void EndPlayerTurn()
    {
        if (!isPlayerTurn)
        {
            return;
        }

        Debug.Log("플레이어의 턴 종료. 선택된 카드를 사용합니다.");
        foreach (var card in useList)
        {
            // 적을 선택하지 않았을 때 선택할 때까지 기다리도록 추가 구현 필요
            if (card.Card.TargetingType == TargetingType.Single && selectedEnemy == null)
            {
                Debug.Log("단일 타겟 카드는 적을 선택해야 합니다.");
                return; // 적이 선택되지 않았다면 함수 종료
            }

            PlayCard(card);
        }

        useList.Clear();
        EnemyTurn();
    }

    public bool IsCardInUseList(CardBehaviour card)
    {
        return useList.Contains(card);
    }

    public void PlayCard(CardBehaviour card)
    {
        List<GameObject> targets = new List<GameObject>();

        switch (card.Card.TargetingType)
        {
            case TargetingType.Single:
                // 단일 타겟: 플레이어가 특정 적을 선택해야 함
                if (selectedEnemy != null)
                {
                    targets.Add(selectedEnemy.gameObject);
                    selectedEnemy = null;
                }
                else
                {
                    Debug.Log("적을 선택해야 합니다.");
                    return;
                }
                break;

            case TargetingType.Multiple:
                // 다중 타겟: maxTargetCount 만큼의 적을 랜덤하게 타겟팅
                int maxTargets = Mathf.Min(card.Card.MaxTargetCount, Enemy.Count);
                int randomTargetCount = Random.Range(1, maxTargets + 1);

                List<int> selectedIndices = new List<int>();
                while (selectedIndices.Count < randomTargetCount)
                {
                    int randomIndex = Random.Range(0, Enemy.Count);
                    if (!selectedIndices.Contains(randomIndex))
                    {
                        selectedIndices.Add(randomIndex);
                        targets.Add(Enemy[randomIndex].gameObject);
                    }
                }
                break;

            default:
                Debug.LogWarning("알 수 없는 타겟팅 타입입니다.");
                return;
        }

        card.Use(targets.ToArray());

        discardDeck.Add(card);
        Debug.Log(card.Card.CardName + " 카드를 사용했습니다.");
    }

    public void ShuffleDiscardToDeck()
    {
        // discardDeck의 모든 카드를 drawDeck으로 이동
        foreach (var card in discardDeck)
        {
            CardManager.Instance.AddCardToDeck(card);
        }
        discardDeck.Clear();

        Debug.Log("discardDeck의 모든 카드를 drawDeck으로 섞었습니다.");
    }

    private void EnemyTurn()
    {
        isPlayerTurn = false;
        Debug.Log("적의 턴입니다.");
        // 적의 행동 추가 로직
        
        EnemyActive(Enemy);
        PlayerTurn();
    }

    // 적 스크립트 상세 구현 후 수정 필요
    private void EnemyActive(List<BaseEnemy> enemy)
    {
        foreach(var e in enemy)
        {
            e.AttackPlayer();
        }
    }
}
