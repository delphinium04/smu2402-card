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
    public List<EntityData> TESTENEMYDATALIST;
    public List<BaseEnemy> enemyList; // 동적으로 적 오브젝트들 생성 구현 필요(맵 스크립트 작성 필요)

    public static BattleManager Instance { get; private set; }

    public enum State
    {
        Idle,
        WaitForCard,
        WaitForTarget
    }

    public State CurrentState { get; private set; } = State.Idle;

    public Action OnTurnPassed = null;

    public int cardDrawAmount = 5;
    int maxCardsPerTurn = 3;

    bool isPlayerTurn = true;
    bool isBattleEnd = true;

    List<CardBehaviour> drewCards; // 플레이어가 현재 턴에서 드로우한 카드
    List<Tuple<CardBehaviour, BaseEnemy>> preservedCardAct;
    public BaseEnemy clickedEnemy;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        CurrentState = State.Idle;
    }

    private void Start()
    {
        enemyList.AddRange(TESTENEMYDATALIST.Select(EnemyManager.GetEnemy));

        // TEST
        StartBattle();
    }

    public void StartBattle()
    {
        StartCoroutine(BattleRoutine());
    }


    void ResetVariables()
    {
        PlayerController.Instance.ResetSetting();

        isPlayerTurn = true;
        isBattleEnd = false;
        CurrentState = State.Idle;

        drewCards = new List<CardBehaviour>();
        preservedCardAct = new List<Tuple<CardBehaviour, BaseEnemy>>();
        enemyList.ForEach(enemy =>
        {
            enemy.OnDeath += OnEnemyDie;
            enemy.OnClicked += OnEnemyClicked;
        });

        UIManager.Instance.OnCardSelectEndButtonClicked += OnCardConfirmBtnClicked;
        UIManager.Instance.OnTurnPassButtonClicked += OnTurnEndButtonClicked;
        // Make Enemy Object by Data (by someone... like GameManager?)
    }

    IEnumerator BattleRoutine()
    {
        ResetVariables();
        Debug.Log("Battle Started");

        while (!isBattleEnd)
        {
            PlayerAct();
            // 턴 종료 전까지 대기
            while (isPlayerTurn) yield return null;
            CheckIsBattleEnd();

            EnemyAct();
            CheckIsBattleEnd();
            yield return null;

            OnTurnPassed?.Invoke();
        }

        if (enemyList.Count == 0)
        {
            Debug.Log("게임 승리");
            // Load ...
        }
        else
        {
            Debug.Log("게임 패배");
            // Load Fail Scene
        }
    }

    private void CheckIsBattleEnd()
    {
        if (PlayerController.Instance.Hp <= 0)
        {
            if (!PlayerController.Instance.isRevived)
                PlayerController.Instance.RevivePlayer();
            else
                isBattleEnd = true;
        }

        if (enemyList.Count == 0 || CardManager.Instance.IsEmpty)
            isBattleEnd = true;
    }

    // 플레이어 행동 시작
    private void PlayerAct()
    {
        Debug.Log("플레이어 턴");
        isPlayerTurn = true;
        drewCards.AddRange(CardManager.Instance.DrawCard(cardDrawAmount));
        if (drewCards.Count <= 0)
        {
            Debug.LogError("No Card in drew cards");
            return;
        }

        for (int i = 0; i < drewCards.Count; i++)
        {
            // Action 등록
            drewCards[i].OnCardClicked += OnCardClicked;

            // x좌표 -5 ~ 5 나열
            drewCards[i].transform.position = Vector3.right * (-5 + (10.0f / drewCards.Count) * i);
            drewCards[i].GetComponent<SpriteRenderer>().sortingOrder = 100 + i;
        }

        CurrentState = State.WaitForCard;
        UIManager.Instance.SetUI(State.WaitForCard);
    }

    // 플레이어 행동 끝
    private void PlayerActEnd()
    {
        isPlayerTurn = false;

        // preservedCardAct에 없는 카드만 다시 복귀
        CardManager.Instance.AddCardToDeck(
            drewCards.Where(
                card => preservedCardAct.All(
                    x => x.Item1 != card)).ToArray());

        drewCards.ForEach(e => Destroy(e.gameObject));
        drewCards.Clear();
        preservedCardAct.Clear();
        
        CheckIsBattleEnd();
    }


    // 카드 클릭 시 Action<CardBehaviour>를 통해 Invoke됨    
    private void OnCardClicked(CardBehaviour c)
    {
        if (!isPlayerTurn || CurrentState != State.WaitForCard)
        {
            Debug.LogWarning("플레이어 턴이 아님 / 선택 불가능");
            return;
        }

        // 이미 선택한 경우
        Tuple<CardBehaviour, BaseEnemy> cardTargetTuple;
        if ((cardTargetTuple = preservedCardAct.Find(x => x.Item1 == c)) != null)
        {
            Debug.Log("카드 취소됨");
            preservedCardAct.Remove(cardTargetTuple);
        }
        else if (preservedCardAct.Count < maxCardsPerTurn)
        {
            Debug.Log("카드 선택됨");
            clickedEnemy = null;

            if (c.Data.TargetingType == TargetingType.Single)
            {
                CurrentState = State.WaitForTarget;
                StartCoroutine(WaitForEnemySelection(c));
            }
            else
            {
                cardTargetTuple = new Tuple<CardBehaviour, BaseEnemy>(c, clickedEnemy);
                preservedCardAct.Add(cardTargetTuple);
            }
        }
        else
            Debug.LogWarning("카드 선택 한계 도달");
    }

    private IEnumerator WaitForEnemySelection(CardBehaviour c)
    {
        Debug.Log("단일 타겟 카드가 선택되었습니다. 적을 선택하세요.");
        CurrentState = State.WaitForTarget;
        UIManager.Instance.SetUI(State.WaitForTarget);
        // 적 클릭 대기
        while (clickedEnemy is null) yield return null;
        preservedCardAct.Add(new Tuple<CardBehaviour, BaseEnemy>(c, clickedEnemy));
        CurrentState = State.WaitForCard;
    }

    void OnEnemyClicked(BaseEnemy e)
    {
        if (CurrentState != State.WaitForTarget) return;
        clickedEnemy = e;
        Debug.Log($"{clickedEnemy.name} clicked");
    }

    // 카드 선택 완료, 타깃 선택 + 카드 정리
    private void OnCardConfirmBtnClicked()
    {
        if (!isPlayerTurn || CurrentState == State.WaitForTarget) return;
        CurrentState = State.Idle;
        UIManager.Instance.SetUI(State.Idle);
        
        Debug.Log("선택한 카드 사용");
        preservedCardAct.ForEach(tuple => { tuple.Item1.Use(tuple.Item2); });
        PlayerActEnd();
    }


    // 카드 실행 및 정리, 턴 종료
    private void OnTurnEndButtonClicked()
    {
        PlayerActEnd();
    }

    // 적 사망 시 Action<BaseEnemy>를 통해 Invoke됨    
    private void OnEnemyDie(BaseEnemy e)
    {
        // Give Item Or Card ...
        enemyList.Remove(e);
        Destroy(e.gameObject);
    }

    private void EnemyAct()
    {
        Debug.Log("적의 턴");
        enemyList.ForEach(e => e.ActivatePattern());
    }
}