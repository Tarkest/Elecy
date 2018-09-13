public static class GSC
{

    #region Camera
    public static float cam_dist = 20f;
    public static float cam_height = 18f;
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

    public const double timerTick = 1000d / 30d;
}

public enum SpellMovement
{
    CasterToPointMovement = 0,
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

