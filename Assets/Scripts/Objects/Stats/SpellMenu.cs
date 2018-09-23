using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="NewSpell", menuName ="Spell")]
public class SpellMenu : BaseStatsMenu
{
    public string SpellName;
    public string Description;
    public string Combination;
    public SpellMovement Movement;
    public int SunergyCost;
    public int Damage;
    public float Distance;
    /// <summary>
    /// Maximum value: 100
    /// Minimum value: 10
    /// </summary>
    public float Speed;
    public float CastTime;
    public float CastArea;
    public Image SpellIcon;
}

