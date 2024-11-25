using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Card;
using Enemy;
using UI;
using Unity.VisualScripting;

/// <summary>
/// 전투 시스템을 관리하는 싱글턴 클래스입니다. 전투의 흐름과 턴 진행을 제어하며, 플레이어와 적 간의 상호작용을 담당합니다.
/// 카드매니저 스크립트를 싱글턴으로 구현했습니다. (플레이어의 덱을 저장함으로써 원본의 덱을 통해 전투를 진행하기 위함)
/// 카드 강화 시스템 미구현 (카드 오브젝트들의 초기 레벨 1로 설정과 카드 레벨업 함수 구현이 되야 강화 시스템 구현 가능합니다.)
/// </summary>
public class BattleManager
{
    const int DefaultCardDrawAmount = 5;
    const int MaxCardsPerTurn = 3;
    readonly Vector3 EnemyStartPos = Vector3.right * 2;
    readonly Vector3 EnemyEndPos = Vector3.right * 7;
    readonly Vector3 CardStartPos = new Vector3(-7, -3, 0);
    readonly Vector3 CardEndPos = new Vector3(4, -3, 0);

    public BattleUI UI;
    public bool IsFinalBattle = false;

    public Action OnTurnPassed = null;

    State _currentState;
    bool _isPlayerTurn = true;
    bool _isBattleEnd = true;

    List<CardBehaviour> _drewCards = new(); // 플레이어가 현재 턴에서 드로우한 카드
    List<Tuple<CardBehaviour, BaseEnemy>> _preservedCardActions = new();

    public List<BaseEnemy> Enemies = new();
    BaseEnemy _selectedEnemy = null; // Use in coroutine

    public enum State
    {
        Idle,
        WaitForCard,
        WaitForTarget
    }

    State CurrentState
    {
        get => _currentState;
        set
        {
            _currentState = value;
            UpdateUIState(value);
        }
    }

    void UpdateUIState(State state)
    {
        UI.SetUI(state);
    }

    // 적 세팅 및 게임 시작
    public void StartBattle(string title, params EnemyData[] data)
    {
        UI = GameObject.Find("BattleUI").GetComponent<BattleUI>();
        for (int i = 0; i < data.Length; i++)
        {
            BaseEnemy e = Managers.Resource.GetEnemy(data[i]);
            AddEnemy(e);
            e.transform.position = Vector3.Lerp(EnemyStartPos, EnemyEndPos, i / (float)data.Length);
        }

        UI.SetTitle(title);
        Managers.RunCoroutine(BattleRoutine());
    }

    void ResetVariables()
    {
        PlayerController.Instance.ResetSetting();
        PlayerController.Instance.BattleStart();
        _isPlayerTurn = true;
        _isBattleEnd = false;
        _drewCards.Clear();
        _preservedCardActions.Clear();
        CurrentState = State.Idle;
        UI.OnPassBtnClicked += OnTurnEndClicked;
        UI.OnCardConfirmBtnClicked += OnCardConfirmClicked;
    }

    IEnumerator BattleRoutine()
    {
        ResetVariables();
        Debug.Log("Battle Started");
        while (!_isBattleEnd)
        {
            PlayerAction();
            while (_isPlayerTurn) yield return null;
            CheckBattleEnd();
            EnemyAction();
            CheckBattleEnd();
            yield return null;
            OnTurnPassed?.Invoke();
        }

        EndBattle();
    }

    private void CheckBattleEnd()
    {
        if (PlayerController.Instance.Hp <= 0)
        {
            if (!PlayerController.Instance.isRevived)
                PlayerController.Instance.RevivePlayer();
            else
                _isBattleEnd = true;
        }
        else if (Enemies.Count == 0 || CardManager.Instance.IsEmpty)
            _isBattleEnd = true;
    }

    private void PlayerAction()
    {
        _isPlayerTurn = true;
        _drewCards.AddRange(CardManager.Instance.DrawCard(DefaultCardDrawAmount));
        if (_drewCards.Count <= 0)
        {
            Debug.LogError("No Card in drew cards");
            return;
        }

        for (int i = 0; i < _drewCards.Count; i++)
        {
            _drewCards[i].OnCardClicked += OnCardClicked;
            _drewCards[i].transform.position =
                Vector3.Lerp(CardStartPos, CardEndPos, (i / (float)_drewCards.Count));
            _drewCards[i].GetComponent<SpriteRenderer>().sortingOrder = 100 + i;
        }

        CurrentState = State.WaitForCard;
        UI.SetUI(State.WaitForCard);
    }

    private void PlayerActionEnd()
    {
        _isPlayerTurn = false;
        // preservedCardAct에 없는 카드만 다시 복귀
        var cardsToReturnToDeck =
            _drewCards.Where(card => _preservedCardActions.All(pair => pair.Item1 != card)).ToArray();
        CardManager.Instance.AddCardToDeck(cardsToReturnToDeck);
        _drewCards.ForEach(card => Managers.Resource.Destroy(card.gameObject));
        _drewCards.Clear();
        _preservedCardActions.Clear();
    }

    private void OnCardClicked(CardBehaviour card)
    {
        if (!_isPlayerTurn || CurrentState != State.WaitForCard)
        {
            Debug.LogWarning("플레이어 턴이 아님 / 선택 불가능");
            return;
        }

        var cardTargetTuple = _preservedCardActions.Find(tuple => tuple.Item1 == card);
        if (cardTargetTuple != null)
        {
            card.UnselectCard(); // Animation
            _preservedCardActions.Remove(cardTargetTuple);
        }
        else if (_preservedCardActions.Count < MaxCardsPerTurn)
        {
            card.SelectCard(); // Animation
            _selectedEnemy = null;
            if (card.Data.TargetingType == TargetingType.Single)
                Managers.RunCoroutine(WaitForEnemySelection(card));
            else
                _preservedCardActions.Add(new Tuple<CardBehaviour, BaseEnemy>(card, _selectedEnemy));
        }
        else
            Debug.LogWarning("카드 선택 한계 도달");
    }

    private IEnumerator WaitForEnemySelection(CardBehaviour card)
    {
        CurrentState = State.WaitForTarget;
        UI.SetUI(State.WaitForTarget);
        
        while (_selectedEnemy is null) yield return null;
        _preservedCardActions.Add(new Tuple<CardBehaviour, BaseEnemy>(card, _selectedEnemy));
        CurrentState = State.WaitForCard;
    }

    void OnEnemyClicked(BaseEnemy enemy)
    {
        if (CurrentState != State.WaitForTarget) return;
        _selectedEnemy = enemy;
    }

    private void OnCardConfirmClicked()
    {
        if (!_isPlayerTurn || CurrentState == State.WaitForTarget) return;
        CurrentState = State.Idle;

        _preservedCardActions.ForEach(tuple => { tuple.Item1.Use(tuple.Item2); });
        PlayerActionEnd();
    }

    public void AddEnemy(BaseEnemy enemy)
    {
        Enemies.Add(enemy);
        enemy.OnClicked += OnEnemyClicked;
        enemy.OnDeath += OnEnemyDie;
    }

    // 카드 실행 및 정리, 턴 종료
    void OnTurnEndClicked()
    {
        PlayerActionEnd();
    }

    // 적 사망 시 Action<BaseEnemy>를 통해 Invoke됨    
    void OnEnemyDie(BaseEnemy e)
    {
        // Give Item Or Card ...
        Enemies.Remove(e);
        Managers.Resource.Destroy(e.gameObject);
    }

    void EnemyAction()
    {
        Enemies.ToList().ForEach(e => e.ActivatePattern());
    }
    
    private void EndBattle()
    {
        PlayerController.Instance.BattleEnd();
        if (Enemies.Count == 0)
        {
            Debug.Log("Victory");
            UI.SetWinUI();
        }
        else
        {
            Debug.Log("Defeat");
            Managers.Game.GameLose();
        }
    }

    void OnEndButtonClicked()
    {
        Managers.Game.EndBattle();
    }
}