using System;
using Enemy;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }
    
    public Action<int, int> OnHpChanged = null;
    
    public string PlayerName { get; private set; } = "black skull";
    readonly int _maxHp = 70;
    public bool isRevived { get; private set; } = false;

    // 동료 의사 여부
    public bool HasDoctor = false;

    // 환경변수 영향 무시 여부와 남은 턴 수
    private bool ignoreEnvironmentEffect = false;
    private int ignoreEnvironmentTurns = 0;

    public bool canSelectNormalCard = true;
    public bool canSelectSkillCard = true;
    public bool canSelectSpecialCard = true;

    protected Sprite image;
    public string Name { get; private set; }

    EntityUI _ui;

    int _hp;
    public int Hp
    {
        get => _hp;
        private set
        {
            _hp = value;
            OnHpChanged?.Invoke(_hp, _maxHp);
        }
    }

    private int _atkMultiplier = 100;

    public int AtkMultiplier
    {
        get => _atkMultiplier;
        set
        {
            _atkMultiplier = value;
            Debug.Log("플레이어의 공격 효과 계수가 " + _atkMultiplier + "%로 설정되었습니다.");
        }
    }

    private int _healMultiplier = 100;

    public int HealMultiplier
    {
        get => _healMultiplier;
        set
        {
            _healMultiplier = value;
            Debug.Log("플레이어의 공격 효과 계수가 " + _healMultiplier + "%로 설정되었습니다.");
        }
    }

    private int _takeMultiplier = 100;

    public int TakeMultiplier
    {
        get => _takeMultiplier;
        set
        {
            _takeMultiplier = value;
            Debug.Log("플레이어의 공격 효과 계수가 " + _takeMultiplier + "%로 설정되었습니다.");
        }
    }

    protected void Awake()
    {
        if(Instance == null) Instance = this;
        else Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);

        Hp = _maxHp;
        _ui = GetComponentInChildren<EntityUI>();
    }

    void Start()
    {
        _ui.UpdateHp(Hp, _maxHp);
        OnHpChanged += _ui.UpdateHp;
        Managers.Battle.OnTurnPassed += EndTurn;
    }

    public void ResetSetting()
    {
        isRevived = false;
        ignoreEnvironmentEffect = false;
        ignoreEnvironmentTurns = 0;
    }

    public void RevivePlayer()
    {
        if (!isRevived && Hp <= 0)
        {
            Hp = HasDoctor ? 30 : 1;
            isRevived = true;
            Debug.Log($"플레이어 부활. 회복 디버프 적용. 부활 후 체력: {Hp}");
        }
    }

    public void Heal(int heal)
    {
        int adjustedHeal = (int)(heal * (_healMultiplier / 100.0f) * (isRevived ? 0.5f : 1));
        Hp += adjustedHeal;
        Hp = Mathf.Min(Hp, _maxHp); // 최대 체력을 초과하지 않도록 설정
        Debug.Log($"플레이어: {adjustedHeal} 체력 회복");
    }

    public void TakeDamage(int damage)
    {
        int value = (int)(damage * TakeMultiplier / 100.0f);
        Hp -= value;
        Debug.Log($"Player: {value} 데미지 입음");
    }

    // 동료 의사 합류했을 때 호출
    public void JoinDoctor()
    {
        HasDoctor = true;
    }

    // 환경변수 3턴간 무시 기능 활성화
    public void ActivateEnvironmentEffectIgnore()
    {
        ignoreEnvironmentEffect = true;
        ignoreEnvironmentTurns = 3;
        Debug.Log("플레이어가 " + 3 + "턴 동안 환경변수를 무시합니다.");
    }

    public void EndTurn()
    {
        if (ignoreEnvironmentTurns > 0)
        {
            ignoreEnvironmentTurns--;

            if (ignoreEnvironmentTurns == 0)
            {
                ignoreEnvironmentEffect = false;
                Debug.Log("환경변수 무시 효과가 종료되었습니다.");
            }
        }

        EndBuff();
    }

    // 1턴마다 버프가 종료되도록 설정 
    public void EndBuff()
    {
        canSelectNormalCard = true;
        canSelectSpecialCard = true;
        canSelectSkillCard = true;
    }
}