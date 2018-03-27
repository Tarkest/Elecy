using UnityEngine.UI;
using UnityEngine;

[CreateAssetMenu(fileName = "New Buff/Debuff", menuName = "Effect")]
public class Effect1 : ScriptableObject {

    public string effectName;
    public string effectDescription;

    public bool isStunning;
    public bool isCasting;
    public bool isStucking;
    public bool isStunStackable;
    public bool isHpModStackable;
    public bool isMsModStackable;

    public int healthMod;
    public int synergyMod;
    public int damageMod;

    public float movespeedMod;
    public float duration;
    public float stunduration;
    public float castduration;
    public float stuckduration;
    public float effectduration;
    public float modFrequency;

    public Image buffIcon;	
}
