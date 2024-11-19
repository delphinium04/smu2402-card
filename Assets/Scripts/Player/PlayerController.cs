using System;
using Entity;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : AbstractEntity
{
    public static PlayerController Instance { get; private set; }
    
    public string PlayerName { get; private set; } = "black skull";
    private int maxHp = 70;

    public bool isRevived { get; private set; } = false; // 플레이어 최초 사망 부활 확인
    
   
    // 동료 의사 여부
    public bool HasDoctor = false;

    // 환경변수 영향 무시 여부와 남은 턴 수
    private bool ignoreEnvironmentEffect = false;
    private int ignoreEnvironmentTurns = 0;

    public bool canSelectNormalCard = true;
    public bool canSelectSkillCard = true;
    public bool canSelectSpecialCard = true;

    public int Gold { get; private set; } = 0;
    private int minGold = 0;

    private void Awake()
    {
        // 싱글턴 인스턴스 설정
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // 씬 전환 시에도 유지되도록 설정
        Init(Resources.Load<EntityData>("Player"));
    }

    void Start()
    {
        BattleManager.Instance.OnTurnPassed += EndTurn;
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
            Debug.Log("플레이어가 부활했습니다. 회복량이 50% 감소됩니다. 부활 후 체력: " + Hp);
        }
    }

    public override void Heal(int heal)
    {
        int adjustedHeal = (int)(heal * (healMultiplier / 100.0f) * (isRevived ? 0.5f : 1));
        Hp += adjustedHeal;
        Hp = Mathf.Min(Hp, maxHp); // 최대 체력을 초과하지 않도록 설정
        Debug.Log($"플레이어: {adjustedHeal} 체력 회복");
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
    
    public void IncreaseGold(int amount)
    {
        Gold += amount;
    }
    public void DecreaseGold(int amount)
    {
        if (Gold > 10)
        { 
            Gold -= amount; 
        }
        else if (Gold <= 10)
        {
            Gold = minGold;
        }
    }
}
