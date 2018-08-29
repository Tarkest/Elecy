using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="NewPlayer", menuName ="Player")]
public class PlayerMenu : ScriptableObject
{
    public int MaxHP;
    public int MaxSN;
    public float BaseMoveSpeed;
    public float BaseAttackSpeed;
    public int playerBaseDefence;
    public int playerBaseFireDefence;
    public int playerBaseEarthDefence;
    public int playerBaseWindDefence;
    public int playerBaseWaterDefence;
    public Image Icon;
}

