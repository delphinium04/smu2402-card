using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    private string playerName = "black skul";
    public string PlayerName => playerName;

    private int maxHp = 70;

    private int hp = 70;
    public int Hp => hp;

    private bool isRevival = false; // 플레이어 최초 사망 부활 확인
    public bool IsRevival => isRevival;

    private bool hasDebuff = false; // 부활 후 회복량 감소 디버프 여부
    public bool HasDebuff => hasDebuff;

    // 공격, 피해, 힐에 곱하는 계수 (기본값 100%)
    private int effectMultiplier = 100;
    public int EffectMultiplier => effectMultiplier;

    // 동료 의사 여부
    public bool HasDoctor = false;

    // 환경변수 영향 무시 여부와 남은 턴 수
    private bool ignoreEnvironmentEffect = false;
    private int ignoreEnvironmentTurns = 0;

    bool canSelectNomalCard = true;
    bool canSelectSkillCard = true;

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
        int adjustedDamage = Mathf.CeilToInt(damage * (effectMultiplier / 100));
        hp -= adjustedDamage;
        Debug.Log("플레이어가 " + adjustedDamage + "의 피해를 입었습니다. 남은 체력: " + hp);
    }

    public void IsRevivalFalseSetting()
    {
        isRevival = false;
        hasDebuff = false;
        ignoreEnvironmentEffect = false;
        ignoreEnvironmentTurns = 0;
    }

    public void RevivalPlayer()
    {
        if (!isRevival && hp <= 0)
        {
            hp = HasDoctor ? 30 : 1;
            isRevival = true;
            hasDebuff = true;
            Debug.Log("플레이어가 부활했습니다. 회복량이 50% 감소됩니다. 부활 후 체력: " + hp);
        }
    }

    public void Heal(int amount)
    {
        if (hasDebuff)
        {
            amount = Mathf.CeilToInt(amount * 0.5f); // 회복량 -50%
            Debug.Log("회복량이 50% 감소되었습니다.");
        }

        int adjustedHeal = Mathf.CeilToInt(amount * (effectMultiplier / 100));
        hp += adjustedHeal;
        hp = Mathf.Min(hp, maxHp); // 최대 체력을 초과하지 않도록 설정
        Debug.Log("플레이어가 " + adjustedHeal + "의 체력을 회복했습니다. 현재 체력: " + hp);
    }

    public void SetEffectMultiplier(int multiplier)
    {
        effectMultiplier = multiplier;
        Debug.Log("플레이어의 효과 계수가 " + multiplier + "%로 설정되었습니다.");
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
        if (ignoreEnvironmentEffect && ignoreEnvironmentTurns > 0)
        {
            ignoreEnvironmentTurns--;

            if (ignoreEnvironmentTurns == 0)
            {
                ignoreEnvironmentEffect = false;
                Debug.Log("환경변수 무시 효과가 종료되었습니다.");
            }
        }
    }

    public bool IsDead()
    {
        Debug.Log("플레이어가 사망했습니다.");
        return (isRevival == true && hp <= 0);
    }
}
