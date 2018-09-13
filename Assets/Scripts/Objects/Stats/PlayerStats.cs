using System;
using UnityEngine;

public class PlayerStats : BaseStats
{
    public int CurrentHP;
    public int CurrentSN;
    public float CurrentMoveSpeed;
    public float CurrentAttackSpeed;
    public int CurrentBaseDefence;
    public int CurrentBaseFireDefence;
    public int CurrentBaseEarthDefence;
    public int CurrentBaseWindDefence;
    public int CurrentBaseWaterDefence;

    internal override void SetBaseStats(BaseObject obj)
    {

        base.SetBaseStats(obj);
        CurrentHP = (stats as PlayerMenu).MaxHP;
        CurrentSN = (stats as PlayerMenu).MaxSN;
        CurrentMoveSpeed = (stats as PlayerMenu).BaseMoveSpeed;
        CurrentAttackSpeed = (stats as PlayerMenu).BaseAttackSpeed;
        CurrentBaseDefence = (stats as PlayerMenu).playerBaseDefence;
        CurrentBaseFireDefence = (stats as PlayerMenu).playerBaseFireDefence;
        CurrentBaseEarthDefence = (stats as PlayerMenu).playerBaseEarthDefence;
        CurrentBaseWindDefence = (stats as PlayerMenu).playerBaseWindDefence;
        CurrentBaseWaterDefence = (stats as PlayerMenu).playerBaseWaterDefence;
    }

    internal override void TakeDamage(int damage)
    {
        Debug.Log("Took " + damage + " damage");
    }

    private void Update()
    {
        foreach (Effect Effect in Effects)
        {
            //if (Effect.InvokeEffect(Time.deltaTime, this))
            //    Effects.Remove(Effect);
        }
    }

}
