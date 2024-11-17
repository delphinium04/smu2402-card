using System;
using System.Collections.Generic;
using UnityEngine;
using Card;
using Unity.VisualScripting;

/// <summary>
/// 전투 시스템을 관리하는 싱글턴 클래스입니다. 전투의 흐름과 턴 진행을 제어하며, 플레이어와 적 간의 상호작용을 담당합니다.
/// 카드매니저 스크립트를 싱글턴으로 구현했습니다. (플레이어의 덱을 저장함으로써 원본의 덱을 통해 전투를 진행하기 위함)
/// 카드 강화 시스템 미구현 (카드 오브젝트들의 초기 레벨 1로 설정과 카드 레벨업 함수 구현이 되야 강화 시스템 구현 가능합니다.)
/// </summary>

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }

    public List<EnemyBehaviour> Enemy = new List<EnemyBehaviour>(); // 동적으로 적 오브젝트들 생성 구현 필요(맵 스크립트 작성 필요)

    private int maxHandSize = 5;
    private int cardsPlayedThisTurn = 0;
    private int maxCardsPerTurn = 3;

    public Action OnTurnPassed = null;

    private bool isPlayerTurn = true;
    public bool IsPlayerTurn => isPlayerTurn;
    
    private List<CardBehaviour> hand = new List<CardBehaviour>(); // 플레이어가 현재 턴에서 드로우한 카드
    private List<CardBehaviour> discardDeck = new List<CardBehaviour>(); // 묘지
    private List<CardBehaviour> useList = new List<CardBehaviour>(); // 플레이어가 현재 턴에서 선택한 사용할 카드

    private EnemyBehaviour selectedEnemy;
    
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
        StartBattle();
    }
    private void Update()
    {
        if (!PlayerController.Instance.isRevived && PlayerController.Instance.Hp <= 0)
        {
            PlayerController.Instance.RevivePlayer();
            PlayerTurn();
        }
        
        // 전투 종료 조건 확인
        if (PlayerController.Instance != null && PlayerController.Instance.IsDead())
        {
            EndBattle(false); // 플레이어가 패배한 경우
        }
        else if (Enemy.TrueForAll(enemy => enemy.IsDead()))
        {
            EndBattle(true); // 모든 적이 사망한 경우, 플레이어의 승리
        }
    }

    private void StartBattle()
    {
        PlayerController.Instance.ResetSetting();
        PlayerTurn();
    }

    private void EndBattle(bool playerWon)
    {
        if (playerWon)
        {
            Debug.Log("플레이어가 승리했습니다!");
            // 플레이어 승리로 전투 종료시 묘지의 카드들 플레이어 덱으로 복구
            ShuffleDiscardToDeck();
        }
        else
        {
            Debug.Log("플레이어가 패배했습니다...");
        }

        // 전투 종료 처리 (게임매니저 스크립트에 결과 전달로 씬 전환 구현 필요) ex) GameManager.Instance.HandleBattleResult(playerWon);
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
    public void SelectEnemy(EnemyBehaviour enemy)
    {
        if (!isPlayerTurn)
        {
            Debug.LogWarning("현재 플레이어의 턴이 아닙니다.");
            return;
        }

        selectedEnemy = enemy;
        Debug.Log("적이 선택되었습니다: " + enemy.EnemyName);
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
        PlayerController.Instance.EndTurn();
        EnemyTurn();
        OnTurnPassed?.Invoke();
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
        
        EnemyActive(Enemy);
        PlayerTurn();
    }

    private void EnemyActive(List<EnemyBehaviour> enemy)
    {
        foreach(var e in enemy)
        {
            e.PlayPattern(e);
        }
    }
}
