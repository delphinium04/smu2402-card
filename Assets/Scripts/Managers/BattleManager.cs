using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Card;
using Enemy;
using Unity.VisualScripting;

/// <summary>
/// 전투 시스템을 관리하는 싱글턴 클래스입니다. 전투의 흐름과 턴 진행을 제어하며, 플레이어와 적 간의 상호작용을 담당합니다.
/// 카드매니저 스크립트를 싱글턴으로 구현했습니다. (플레이어의 덱을 저장함으로써 원본의 덱을 통해 전투를 진행하기 위함)
/// 카드 강화 시스템 미구현 (카드 오브젝트들의 초기 레벨 1로 설정과 카드 레벨업 함수 구현이 되야 강화 시스템 구현 가능합니다.)
/// </summary>
public class BattleManager
{
    public enum State
    {
        Idle,
        WaitForCard,
        WaitForTarget
    }

    public State CurrentState { get; private set; } = State.Idle;

    public Action OnTurnPassed = null;

    public int cardDrawAmount = 5;
    int _maxCardsPerTurn = 3;

    bool _isPlayerTurn = true;
    bool _isBattleEnd = true;

    List<CardBehaviour> _drewCards; // 플레이어가 현재 턴에서 드로우한 카드
    List<Tuple<CardBehaviour, BaseEnemy>> _preservedCardAct;

    public List<BaseEnemy> enemyList;
    BaseEnemy _clickedEnemy;

    public BattleManager()
    {
        _drewCards = new List<CardBehaviour>();
        _preservedCardAct = new List<Tuple<CardBehaviour, BaseEnemy>>();
        enemyList = new List<BaseEnemy>();
        _clickedEnemy = null;
    }

    // 적 세팅 및 게임 시작
    public void StartBattle()
    {
        var tempEnemy = Managers.Resource.GetEnemy(Managers.Resource.Load<EnemyData>("Enemy/Boatswain"));
        tempEnemy.transform.position = Vector3.right * 8;
        enemyList.Add(tempEnemy);
        
        Managers.RunCoroutine(BattleRoutine());
    }

    void ResetVariables()
    {
        PlayerController.Instance.ResetSetting();

        _isPlayerTurn = true;
        _isBattleEnd = false;
        CurrentState = State.Idle;

        _drewCards = new List<CardBehaviour>();
        _preservedCardAct = new List<Tuple<CardBehaviour, BaseEnemy>>();
        enemyList.ForEach(enemy =>
        {
            enemy.OnDeath += OnEnemyDie;
            enemy.OnClicked += OnEnemyClicked;
        });

        Managers.UI.OnPassBtnClicked += OnTurnEndButtonClicked;
        Managers.UI.OnCardConfirmBtnClicked += OnCardConfirmBtnClicked;
        // Make Enemy Object by Data (by someone... like GameManager?)
    }

    IEnumerator BattleRoutine()
    {
        ResetVariables();
        Debug.Log("Battle Started");

        while (!_isBattleEnd)
        {
            PlayerAct();
            // 턴 종료 전까지 대기
            while (_isPlayerTurn) yield return null;
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
                _isBattleEnd = true;
        }

        if (enemyList.Count == 0 || CardManager.Instance.IsEmpty)
            _isBattleEnd = true;
    }

    // 플레이어 행동 시작
    private void PlayerAct()
    {
        Debug.Log("플레이어 턴");
        _isPlayerTurn = true;
        _drewCards.AddRange(CardManager.Instance.DrawCard(cardDrawAmount));
        if (_drewCards.Count <= 0)
        {
            Debug.LogError("No Card in drew cards");
            return;
        }

        for (int i = 0; i < _drewCards.Count; i++)
        {
            // Action 등록
            _drewCards[i].OnCardClicked += OnCardClicked;

            // x좌표 -5 ~ 5 나열
            _drewCards[i].transform.position = Vector3.right * (-5 + (10.0f / _drewCards.Count) * i);
            _drewCards[i].GetComponent<SpriteRenderer>().sortingOrder = 100 + i;
        }

        CurrentState = State.WaitForCard;
        Managers.UI.SetUI(State.WaitForCard);
    }

    // 플레이어 행동 끝
    private void PlayerActEnd()
    {
        _isPlayerTurn = false;

        // preservedCardAct에 없는 카드만 다시 복귀
        CardManager.Instance.AddCardToDeck(
            _drewCards.Where(
                card => _preservedCardAct.All(
                    x => x.Item1 != card)).ToArray());

        _drewCards.ForEach(e => Managers.Resource.Destroy(e.gameObject));
        _drewCards.Clear();
        _preservedCardAct.Clear();

        CheckIsBattleEnd();
    }


    // 카드 클릭 시 Action<CardBehaviour>를 통해 Invoke됨    
    private void OnCardClicked(CardBehaviour c)
    {
        if (!_isPlayerTurn || CurrentState != State.WaitForCard)
        {
            Debug.LogWarning("플레이어 턴이 아님 / 선택 불가능");
            return;
        }

        // 이미 선택한 경우
        Tuple<CardBehaviour, BaseEnemy> cardTargetTuple;
        if ((cardTargetTuple = _preservedCardAct.Find(x => x.Item1 == c)) != null)
        {
            Debug.Log("카드 취소됨");
            _preservedCardAct.Remove(cardTargetTuple);
        }
        else if (_preservedCardAct.Count < _maxCardsPerTurn)
        {
            Debug.Log("카드 선택됨");
            _clickedEnemy = null;

            if (c.Data.TargetingType == TargetingType.Single)
            {
                CurrentState = State.WaitForTarget;
                Managers.RunCoroutine(WaitForEnemySelection(c));
            }
            else
            {
                cardTargetTuple = new Tuple<CardBehaviour, BaseEnemy>(c, _clickedEnemy);
                _preservedCardAct.Add(cardTargetTuple);
            }
        }
        else
            Debug.LogWarning("카드 선택 한계 도달");
    }

    private IEnumerator WaitForEnemySelection(CardBehaviour c)
    {
        Debug.Log("단일 타겟 카드가 선택되었습니다. 적을 선택하세요.");
        CurrentState = State.WaitForTarget;
        Managers.UI.SetUI(State.WaitForTarget);
        // 적 클릭 대기
        while (_clickedEnemy is null) yield return null;
        _preservedCardAct.Add(new Tuple<CardBehaviour, BaseEnemy>(c, _clickedEnemy));
        CurrentState = State.WaitForCard;
    }

    void OnEnemyClicked(BaseEnemy e)
    {
        if (CurrentState != State.WaitForTarget) return;
        _clickedEnemy = e;
        Debug.Log($"{_clickedEnemy.name} clicked");
    }

    // 카드 선택 완료, 타깃 선택 + 카드 정리
    private void OnCardConfirmBtnClicked()
    {
        if (!_isPlayerTurn || CurrentState == State.WaitForTarget) return;
        CurrentState = State.Idle;
        Managers.UI.SetUI(State.Idle);

        Debug.Log("선택한 카드 사용");
        Debug.Log(_preservedCardAct.Count);
        _preservedCardAct.ForEach(tuple => { tuple.Item1.Use(tuple.Item2); });
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
        Managers.Resource.Destroy(e.gameObject);
    }

    private void EnemyAct()
    {
        Debug.Log("적의 턴");
        enemyList.ForEach(e => e.ActivatePattern());
    }
}