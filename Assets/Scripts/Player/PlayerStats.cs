using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {


    public int playerMaxHP = 100;
    public int playerMaxSN = 1000;
    public string fraction;
    [System.NonSerialized]
    public int playerCurrentHP;
    [System.NonSerialized]
    public int playerCurrentSN;
    [System.NonSerialized]
    public string playerCurrentState;
    private Image hpIndicator;
    private Image snIndicator;


    void Start () {
        playerCurrentHP = playerMaxHP;
        playerCurrentSN = playerMaxSN;
        playerCurrentState = "normal";
        hpIndicator = GameObject.Find("HPIndicator").GetComponent<Image>();
        snIndicator = GameObject.Find("SNIndicator").GetComponent<Image>();
    }
	
	void Update () {
        hpIndicator.fillAmount = ((float)playerCurrentHP / (float)playerMaxHP);
        snIndicator.fillAmount = ((float)playerCurrentSN / (float)playerMaxSN);
	}
    public void PlayerSynergyUpdate(int x)
    {
        playerCurrentSN += x;
    }

    public void PlayerSynergyUpdate(float x, string type)
    {
        if(type == "Full")
            playerCurrentSN += Convert.ToInt32(((x / 100) * playerMaxSN));
        else if (type == "Current")
            playerCurrentSN += Convert.ToInt32(((x / 100) * playerCurrentSN));
    }
    
    public void PlayerHealthPointUpdate(int x)
    {
        playerCurrentHP += x;
    }

    public void PlayerHealthPointUpdate(float x, string type)
    {
        if (type == "Full")
            playerCurrentHP += Convert.ToInt32(((x / 100) * playerMaxHP));
        else if (type == "Current")
            playerCurrentHP += Convert.ToInt32(((x / 100) * playerCurrentHP));
    }
}
