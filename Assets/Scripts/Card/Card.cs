using UnityEngine;

public enum CardType
{
    Normal,
    Skill,
    Special
}

public enum CardLevel
{
    One,
    Two,
    Three
}

public enum TargetingType
{
    None,
    Single,
    Multiple,
    All
}
/// <summary>
/// 카드의 추상 클래스로 데이터의 모음입니다.
/// 카드의 실체를 참조할 땐 웬만하면 CardBehaviour를 참조해주세요.
/// </summary>
public abstract class Card : ScriptableObject
{
    [SerializeField]
    private string cardName;
    public string CardName => cardName;

    [SerializeField]
    private Sprite image;
    public Sprite Image => image;

    [SerializeField]
    [TextArea]
    private string description;
    public string Description => description;

    [SerializeField]
    private CardType cardType;
    public CardType CardType => cardType;

    [SerializeField]
    private CardLevel cardLevel;
    public CardLevel CardLevel => cardLevel;

    [SerializeField]
    private TargetingType targetingType;
    public TargetingType TargetingType => targetingType;

    // When TargetingType is multiple
    [SerializeField]
    private int maxTargetCount;
    public int MaxTargetCount => maxTargetCount;

    /// <returns>True when used</returns>
    public abstract void Use(params GameObject[] targets);
}
