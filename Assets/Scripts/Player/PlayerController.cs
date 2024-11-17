using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }
    
    public string PlayerName { get; private set; } = "black skull";
    private int maxHp = 70;
    public int Hp { get; private set; } = 70;

    public bool isRevived { get; private set; } = false; // 플레이어 최초 사망 부활 확인
    public bool HasDebuff { get; private set; } = false; // 부활 후 회복량 감소 디버프 여부
    
    // 공격, 피해, 힐에 곱하는 계수 (기본값 100%)
    private int attackMultiplier = 100;
    public int AttackMultiplier
    {
        get => attackMultiplier;
        set
        {
            attackMultiplier = value;
            Debug.Log("플레이어의 공격 효과 계수가 " + attackMultiplier + "%로 설정되었습니다.");
        }
    }

    private int healMultiplier = 100;
    public int HealMultiplier
    {
        get => healMultiplier;
        set
        {
            healMultiplier = value;
            Debug.Log("플레이어의 회복 효과 계수가 " + healMultiplier + "%로 설정되었습니다.");
        }
    }
    private int takeMultiplier = 100;
    public int TakeMultiplier
    {
        get => takeMultiplier;
        set
        {
            takeMultiplier = value;
            Debug.Log("플레이어의 피해 효과 계수가 " + takeMultiplier + "%로 설정되었습니다.");
        }
    }
    
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
    }

    public void TakeDamage(int damage)
    {
        int adjustedDamage = Mathf.CeilToInt(damage * (takeMultiplier / 100));
        Hp -= adjustedDamage;
        Debug.Log("플레이어가 " + adjustedDamage + "의 피해를 입었습니다. 남은 체력: " + Hp);
    }

    public void ResetSetting()
    {
        isRevived = false;
        HasDebuff = false;
        ignoreEnvironmentEffect = false;
        ignoreEnvironmentTurns = 0;
    }

    public void RevivePlayer()
    {
        if (!isRevived && Hp <= 0)
        {
            Hp = HasDoctor ? 30 : 1;
            isRevived = true;
            HasDebuff = true;
            Debug.Log("플레이어가 부활했습니다. 회복량이 50% 감소됩니다. 부활 후 체력: " + Hp);
        }
    }

    public void Heal(int amount)
    {
        if (HasDebuff)
        {
            amount = Mathf.CeilToInt(amount * 0.5f); // 회복량 -50%
            Debug.Log("회복량이 50% 감소되었습니다.");
        }

        int adjustedHeal = Mathf.CeilToInt(amount * (healMultiplier / 100));
        Hp += adjustedHeal;
        Hp = Mathf.Min(Hp, maxHp); // 최대 체력을 초과하지 않도록 설정
        Debug.Log("플레이어가 " + adjustedHeal + "의 체력을 회복했습니다. 현재 체력: " + Hp);
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

    public bool IsDead()
        => (isRevived == true && Hp <= 0);
}
