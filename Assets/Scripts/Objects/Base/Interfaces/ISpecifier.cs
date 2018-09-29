public interface IStatsMenuSpecifier<SM> where SM : BaseStatsMenu
{
    SM Stats { get; }
}

public interface IBaseObjectSpecifier<O> where O : BaseObject
{
    O BaseObject { get; }
}

public interface IPositionUpdateSpecifier<P> where P : PositionUpdate
{
    P PositionUpdate { get; }
}

public interface IBaseDamageSpecifier<D> where D : BaseDamage
{
    D BaseDamage { get; }
}


