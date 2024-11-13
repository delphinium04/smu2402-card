namespace Card
{
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
        None, // 타겟이 필요 없는 경우
        Single,
        Multiple,
        All
    }

    public enum CardAssetType
    {
        Gun, Sword, Beer, Orange, Blade, BuffNaming, PoisonSword, DebuffNaming, GroupLife, BeerParty
    }
}
