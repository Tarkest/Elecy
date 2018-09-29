public static class GSC
{

    #region Camera
    public static float cam_dist = 10f;
    public static float cam_height = 28f;
    public static float cam_target_height = 0.5f;

    #endregion

    #region Player

        #region Moving/Turning

        public static float speed = 30f;
        public static float player_Y = 0.5f;

    #endregion

    #endregion

    #region Races

    public const int IgnisSpellCount = 9;

    #endregion

    public const double timerTick = 1000d / 50d;
}

public enum PositionUpdateEnum
{
    ToPoint = 0,
    ToObject,
    Empty,
}

public enum RotationUpdateEnum
{
    ToPoint= 0,
    ToObject,
    Empty,
}

public enum ObjectType
{
    player = 1,
    staticObject = 2,
    spell = 3,
}

public enum StaticTypes
{
    tree = 1,
    rock = 2,
}

public enum Tags
{
    Player,
    Enemy,
    Spell, // enemy or alliaed (need to change load spells, bitches)
    StaticObject,
    Untagged,
}

public enum DamageInvokeType
{
    OnHit,
    OnDeath,
    Prolonged,
    Massive,
    AfterTime
}

public enum DamageSerialize
{
    Single,
    Area
}

public enum DamageTargets
{
    Enemy,
    Ally,
    All
}

