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
        None, // 타겟 필요 없음 (예: 플레이어 대상) 
        Single, // 적 단 한 명 필요
        All // 랜덤 적용 (적 전체 필요)
    }

    public enum CardAssetType
    {
        Gun, Sword, Beer, Orange, Blade, BuffNaming, PoisonSword, DebuffNaming, GroupLife, BeerParty
    }
}
