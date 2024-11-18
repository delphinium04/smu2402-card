using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Card;
using Enemy;
using Entity;
using Unity.VisualScripting;

/// <summary>
/// 전투 시스템을 관리하는 싱글턴 클래스입니다. 전투의 흐름과 턴 진행을 제어하며, 플레이어와 적 간의 상호작용을 담당합니다.
/// 카드매니저 스크립트를 싱글턴으로 구현했습니다. (플레이어의 덱을 저장함으로써 원본의 덱을 통해 전투를 진행하기 위함)
/// 카드 강화 시스템 미구현 (카드 오브젝트들의 초기 레벨 1로 설정과 카드 레벨업 함수 구현이 되야 강화 시스템 구현 가능합니다.)
/// </summary>
public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }

    public List<EntityData> TESTENEMYDATALIST;
    
    public List<BaseEnemy> enemyList = new List<BaseEnemy>(); // 동적으로 적 오브젝트들 생성 구현 필요(맵 스크립트 작성 필요)

    private int cardDrawAmount = 5;
    private int cardsPlayedThisTurn = 0;
    private int maxCardsPerTurn = 3;

    public Action OnTurnPassed = null;

    private bool isPlayerTurn = true;
    bool isBattleEnd = true;

    private List<CardBehaviour> drewCards = new List<CardBehaviour>(); // 플레이어가 현재 턴에서 드로우한 카드
    private List<CardBehaviour> selectedCards = new List<CardBehaviour>(); // 플레이어가 현재 턴에서 선택한 사용할 카드

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
        enemyList.AddRange(TESTENEMYDATALIST.Select(EnemyManager.GetEnemy));
        // TEST
        StartCoroutine(BattleRoutine());
    }

    public IEnumerator BattleRoutine()
    {
        PlayerController.Instance.ResetSetting();

        isPlayerTurn = true;
        isBattleEnd = false;

        enemyList.ForEach(enemy => { enemy.OnDeath += OnEnemyDie; });

        Debug.Log("Battle Started");
        
        while (!isBattleEnd)
        {
            OnTurnPassed?.Invoke();
            PlayerTurn();
            while (isPlayerTurn) yield return null;
            EnemyTurn();
            yield return null;
        }
        
        yield return null;
    }

    private void OnEnemyDie(BaseEnemy e)
    {
        // Give Item Or Card ...
        enemyList.Remove(e);
        Destroy(e.gameObject);
        CheckEnd();
    }

    private void CheckEnd()
    {
        if (PlayerController.Instance.Hp <= 0)
        {
            if (!PlayerController.Instance.isRevived)
                PlayerController.Instance.RevivePlayer();
            else
            {
                EndBattle(false);
            }

            return;
        }

        if (enemyList.Count <= 0)
            EndBattle(true);
    }

    private void EndBattle(bool playerWon)
    {
        if (playerWon)
        {
            Debug.Log("플레이어가 승리했습니다!");
            // 플레이어 승리로 전투 종료시 묘지의 카드들 플레이어 덱으로 복구
        }
        else
        {
            Debug.Log("플레이어가 패배했습니다...");
        }
    }

    private void PlayerTurn()
    {
        isPlayerTurn = true;
        cardsPlayedThisTurn = 0;
        drewCards = new List<CardBehaviour>(CardManager.Instance.DrawCard(cardDrawAmount));
        selectedCards.Clear();
        Debug.Log("플레이어의 턴입니다. 카드를 사용하세요.");
        // 카드 사용 후 isPlayerTurn = false;
    }

    // 이번턴에 사용할 카드(핸드 카드 오브젝트들 클릭 또는 드래그로 이벤트 함수로 호출)
    public void SelectCardForUse(CardBehaviour card)
    {
        if (!isPlayerTurn)
        {
            Debug.LogWarning("현재 플레이어의 턴이 아닙니다.");
            return;
        }

        if (selectedCards.Count >= maxCardsPerTurn)
        {
            Debug.Log("이번 턴에 사용할 수 있는 최대 카드 수에 도달했습니다.");
            return;
        }

        if (drewCards.Contains(card))
        {
            selectedCards.Add(card);
            drewCards.Remove(card);
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
        if (selectedCards.Contains(card))
        {
            selectedCards.Remove(card);
            drewCards.Add(card);
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

        // Debug.Log("플레이어의 턴 종료. 선택된 카드를 사용합니다.");
        // foreach (var card in useList)
        // {
        //     // 적을 선택하지 않았을 때 선택할 때까지 기다리도록 추가 구현 필요
        //     if (card.Card.TargetingType == TargetingType.Single && selectedEnemy == null)
        //     {
        //         Debug.Log("단일 타겟 카드는 적을 선택해야 합니다.");
        //         return; // 적이 선택되지 않았다면 함수 종료
        //     }
        //
        //     PlayCard(card);
        // }

        selectedCards.Clear();
        PlayerController.Instance.EndTurn();
        EnemyTurn();
        OnTurnPassed?.Invoke();
    }

    public bool IsCardInUseList(CardBehaviour card)
    {
        return selectedCards.Contains(card);
    }

    public void PlayCard(CardBehaviour card)
    {
        List<GameObject> targets = new List<GameObject>();
    }

    private void EnemyTurn()
    {
        Debug.Log("적의 턴입니다.");
        enemyList.ForEach(e => e.ActivatePattern());
        isPlayerTurn = true;
    }
}