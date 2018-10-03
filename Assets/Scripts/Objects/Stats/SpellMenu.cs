using System;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewSpell", menuName = "Spell")]
public class SpellMenu : BaseStatsMenu
{
    public string SpellName;
    public string Description;
    public string Combination;
    public int SunergyCost;
    public int PhisicDamage;
    public int IgnisDamage;
    public int TerraDamage;
    public int AquaDamage;
    public int CaeliDamage;
    public int PureDamage;
    public bool Heal;
    public float Distance;
    /// <summary>
    /// Maximum value: 100
    /// Minimum value: 10
    /// </summary>
    public float Speed;
    public float CastTime;
    public float CastArea;
    public Image SpellIcon;

    public PositionUpdateEnum Movement = PositionUpdateEnum.Empty;
    public RotationUpdateEnum Rotation = RotationUpdateEnum.Empty;

    public void SetMovement(PositionUpdate value)
    {
        if (value is PositionToObjectUpdate)
            Movement = PositionUpdateEnum.ToObject;
        else if (value is PositionToPointUpdate)
            Movement = PositionUpdateEnum.ToPoint;
    }

    public void SetRotation(RotationUpdate value)
    {
        if (value is RotationToObjectUpdate)
            Rotation = RotationUpdateEnum.ToObject;
        else if (value is RotationToPointUpdate)
            Rotation = RotationUpdateEnum.ToPoint;
    }
}

