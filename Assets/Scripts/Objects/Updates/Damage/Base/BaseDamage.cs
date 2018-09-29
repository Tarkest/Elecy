using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDamage : MonoBehaviour {

    public DamageInvokeType Type;
    public DamageSerialize Serialize;
    public DamageTargets Targets;
    public bool DeathAfterDamage;
    protected int PhysicDamage;
    protected int IgnisDamage;
    protected int TerraDamage;
    protected int AquaDamage;
    protected int CaeliDamage;
    protected int PureDamage;
    protected Spell Spell;

    #region Unity
    public BaseDamage(int PhysicDamage, int IgnisDamage, int TerraDamage, int AquaDamage, int CaeliDamage, int PureDamage)
    {
        this.PhysicDamage = PhysicDamage;
        this.IgnisDamage = IgnisDamage;
        this.TerraDamage = TerraDamage;
        this.AquaDamage = AquaDamage;
        this.CaeliDamage = CaeliDamage;
        this.PureDamage = PureDamage;
    } 

    #endregion
}
