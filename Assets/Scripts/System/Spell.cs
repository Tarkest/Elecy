using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Spell", menuName = "Spell")] 
public class Spell : ScriptableObject {

    public string spellName;
    public string description;
    public int spellHP;
    public int sunergyCount;
    public int damage;
    public float distance;
    public float speed;
    public float castTime;
    public float castArea;
    public Image spellIcon;

}
