using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="NewSpell", menuName ="Spell")]
internal class SpellMenu : ScriptableObject
{
    public string spellName;
    public string description;
    public string combination;
    public StartTransform targetType;
    public int spellMaxHP;
    public int sunergyCost;
    public int damage;
    public float distance;
    public float speed;
    public float castTime;
    public float castArea;
    public Image spellIcon;
}

