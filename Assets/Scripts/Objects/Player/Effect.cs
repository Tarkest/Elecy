using UnityEngine.UI;
using UnityEngine;

[CreateAssetMenu(fileName = "New Effect", menuName = "Effect")]
public class Effect : ScriptableObject {

    //public string effectName;
    //public string effectDescription;

    //public bool isStunning;
    //public bool isCasting;
    //public bool isStucking;
    //public bool isSylensing;
    //public bool isStunStackable;
    //public bool isModTicking;
    //public bool isMsModStackable;

    //public int healthMod;
    //public int synergyMod;
    //public int damageMod;
    //public int basicDefMod;
    //public int fireDefMod;
    //public int earthDefMod;
    //public int windDefMod;
    //public int waterDefMod;

    //public float movespeedMod;
    //public float attackSpeedMod;
    //public float duration;
    //public float modFrequency;
    //public float tickDuration;
    //private float logicTick;

    //public Image buffIcon;

    //private bool isOn = false;

    //public bool InvokeEffect(float deltaTime, PlayerStats player)
    //{
    //    if(!isOn)
    //    {
    //        player.isStunned += isStunning ? 1 : 0;
    //        player.isCasting += isCasting ? 1 : 0;
    //        player.isStucked += isStucking ? 1 : 0;
    //        player.isSylensed += isSylensing ? 1 : 0;
    //        player.playerCurrentHP += healthMod;
    //        player.playerCurrentSN += synergyMod;
    //        player.playerMoveSpeed += movespeedMod;
    //        player.playerAttackSpeed += attackSpeedMod;
    //        player.playerBasicDefence += basicDefMod;
    //        player.playerFireDefence += fireDefMod;
    //        player.playerEarthDefence += earthDefMod;
    //        player.playerWindDefence += windDefMod;
    //        player.playerWaterDefence += waterDefMod;
    //        if (tickDuration == 0)
    //            logicTick = duration / modFrequency;
    //        else
    //            logicTick = tickDuration;
    //        isOn = true;
    //    }

    //    duration -= deltaTime;
    //    if(isModTicking)
    //    {
    //        logicTick -= deltaTime;
    //        if(logicTick <= 0)
    //        {
    //            player.playerCurrentHP += healthMod;
    //            player.playerCurrentSN += synergyMod;
    //            if (modFrequency != 0 && tickDuration == 0)
    //            {
    //                player.playerBasicDefence -= basicDefMod / (int)modFrequency;
    //                player.playerFireDefence -= fireDefMod / (int)modFrequency;
    //                player.playerEarthDefence -= earthDefMod / (int)modFrequency;
    //                player.playerWindDefence -= windDefMod / (int)modFrequency;
    //                player.playerWaterDefence -= waterDefMod / (int)modFrequency;
    //            }
    //            if (tickDuration == 0)
    //                logicTick = duration / modFrequency;
    //            else
    //                logicTick = tickDuration;
    //        }
    //    }

    //    if(duration <= 0f)
    //    {
    //        player.isStunned -= isStunning ? 1 : 0;
    //        if (isCasting)
    //        {
    //            player.isCasting -= isCasting ? 1 : 0;
    //            player.castSuccses = true;
    //        }
    //        player.isStucked -= isStucking ? 1 : 0;
    //        player.isSylensed -= isSylensing ? 1 : 0;
    //        if (movespeedMod != 0) player.playerMoveSpeed -= movespeedMod;
    //        if (attackSpeedMod != 0) player.playerAttackSpeed -= attackSpeedMod;
    //        if (!isModTicking)
    //        {
    //            player.playerBasicDefence -= basicDefMod;
    //            player.playerFireDefence -= fireDefMod;
    //            player.playerEarthDefence -= earthDefMod;
    //            player.playerWindDefence -= windDefMod;
    //            player.playerWaterDefence -= waterDefMod;
    //        }
    //        return true;
    //    }
    //    return false;
    //} 
}
