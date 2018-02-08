using UnityEngine.UI;
using UnityEngine;

public class EffectController : MonoBehaviour {

    public Effect effect;

    private bool isStunned;
    private bool isCast;
    private bool isStucked;
    private int healthMod;
    private int synergyMod;
    private int damageMod;
    private float movespeedMod;
    private float duration;
    private float modFrequency;
    private Image buffIcon;

    private float _durationCounter = 0f;

	public void EffectLoad () {
        isStunned = effect.isStunning;
        isCast = effect.isCasting;
        isStucked = effect.isStucking;
        healthMod = effect.healthMod;
        synergyMod = effect.synergyMod;
        damageMod = effect.damageMod;
        movespeedMod = effect.movespeedMod;
        duration = effect.duration;
        buffIcon = effect.buffIcon;
        modFrequency = effect.modFrequency;
    }
	

	void Update () {
        _durationCounter += Time.deltaTime;

        if (_durationCounter >= duration)
            RemoveEffect();
	}

    void StatsModify()
    {
        GetComponent<PlayerMovement>().speed += movespeedMod;
    }

    void FrequentChange()
    {

    }

    void RemoveEffect()
    {
        GetComponent<PlayerMovement>().speed -= movespeedMod;
        Destroy(this);
    }
}
