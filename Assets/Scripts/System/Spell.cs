using UnityEngine;

[CreateAssetMenu(fileName = "New Spell", menuName = "Spell")] 
public class Spell : ScriptableObject {

    public string spellName;
    public int spellHP;
    public int sunergyCount;
}
