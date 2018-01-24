using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {

    public string fraction;
    public int playerHP;
    public int playerSN;
    [System.NonSerialized]
    public string playerCurrentState;
    Image hpIndicator;
    Image snIndicator;


    void Start () {
        playerHP = 100;
        playerSN = 0;
        playerCurrentState = "normal";
        hpIndicator = GameObject.Find("HPIndicator").GetComponent<Image>();
        snIndicator = GameObject.Find("SNIndicator").GetComponent<Image>();
    }
	
	void Update () {
        hpIndicator.fillAmount = playerHP / 100;
        snIndicator.fillAmount = playerSN / 100; 
	}
}
