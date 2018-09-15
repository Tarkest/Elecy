public interface IStatsSpecifier<S> where S : BaseStats
{
    S Stats { get; }
}

public interface IStatsMenuSpecifier<SM> where SM : BaseStatsMenu
{
    SM Stats { get; }
}

public interface IBaseObjectSpecifier<O> where O : BaseObject
{
    O BaseObject { get; }
}

public interface IMovementSpecifier<M> where M : BaseMovement
{
    M Movement { get; }
}


