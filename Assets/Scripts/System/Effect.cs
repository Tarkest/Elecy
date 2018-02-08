using UnityEngine.UI;
using UnityEngine;

[CreateAssetMenu(fileName = "New Buff/Debuff", menuName = "Effect")]
public class Effect : ScriptableObject {

    public string effectName;
    public string effectDescription;
    public bool isStunning;
    public bool isCasting;
    public bool isStucking;
    public int healthMod;
    public int synergyMod;
    public int damageMod;
    public float movespeedMod;
    public float duration;
    public float modFrequency;
    public Image buffIcon;
	
}
