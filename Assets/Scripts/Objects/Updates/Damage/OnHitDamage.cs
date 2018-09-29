using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHitDamage : BaseDamage
{
    public OnHitDamage(int PhysicDamage, int IgnisDamage, int TerraDamage, int AquaDamage, int CaeliDamage, int PureDamage) 
        : base(PhysicDamage, IgnisDamage, TerraDamage, AquaDamage, CaeliDamage, PureDamage)
    {   }

    private void OnCollisionEnter(Collision collision)
    {
        if(Targets == DamageTargets.Enemy && !collision.gameObject.GetComponent<BaseObject>().isAlly )
        {
            if(Serialize == DamageSerialize.Area)
            {
                RaycastHit[] hits;
                hits = Physics.SphereCastAll(transform.position, Spell.Stats.CastArea, Vector3.up);
                foreach (RaycastHit hit in hits)
                {
                    if (hit.transform.gameObject.GetComponent<BaseObject>() != null)
                    {
                        if (hit.transform.gameObject.GetComponent<BaseObject>() is Player && !hit.transform.gameObject.GetComponent<BaseObject>().isAlly)
                        {
                            SendDataTCP.SendDamage(
                                                    ObjectType.player,
                                                    hit.transform.gameObject.GetComponent<BaseObject>().index,
                                                    PhysicDamage,
                                                    IgnisDamage,
                                                    TerraDamage,
                                                    AquaDamage,
                                                    CaeliDamage,
                                                    PureDamage
                                                   );
                        }
                        if (hit.transform.gameObject.GetComponent<BaseObject>() is Spell && !hit.transform.gameObject.GetComponent<BaseObject>().isAlly)
                        {
                            SendDataTCP.SendDamage(
                                                    ObjectType.spell,
                                                    hit.transform.gameObject.GetComponent<BaseObject>().index,
                                                    PhysicDamage,
                                                    IgnisDamage,
                                                    TerraDamage,
                                                    AquaDamage,
                                                    CaeliDamage,
                                                    PureDamage
                                                   );
                        }
                        if (DeathAfterDamage)
                        {
                            SendDataTCP.SendDestroy(ObjectType.spell, Spell.index);
                        }
                    }
                }
            }
            if(Serialize == DamageSerialize.Single)
            {
                if (collision.gameObject.GetComponent<BaseObject>() != null)
                {
                    if (collision.gameObject.GetComponent<BaseObject>() is Player)
                    {
                        SendDataTCP.SendDamage(
                                                ObjectType.player,
                                                collision.gameObject.GetComponent<BaseObject>().index,
                                                PhysicDamage,
                                                IgnisDamage,
                                                TerraDamage,
                                                AquaDamage,
                                                CaeliDamage,
                                                PureDamage
                                               );
                    }
                    if (collision.gameObject.GetComponent<BaseObject>() is Spell)
                    {
                        SendDataTCP.SendDamage(
                                                ObjectType.spell,
                                                collision.gameObject.GetComponent<BaseObject>().index,
                                                PhysicDamage,
                                                IgnisDamage,
                                                TerraDamage,
                                                AquaDamage,
                                                CaeliDamage,
                                                PureDamage
                                               );
                    }
                }
                if (DeathAfterDamage)
                {
                    SendDataTCP.SendDestroy(ObjectType.spell, Spell.index);
                }
            }
        }

        if(Targets == DamageTargets.Ally && collision.gameObject.GetComponent<BaseObject>().isAlly)
        {
            if (Serialize == DamageSerialize.Area)
            {
                RaycastHit[] hits;
                hits = Physics.SphereCastAll(transform.position, Spell.Stats.CastArea, Vector3.up);
                foreach (RaycastHit hit in hits)
                {
                    if (hit.transform.gameObject.GetComponent<BaseObject>() != null)
                    {
                        if (hit.transform.gameObject.GetComponent<BaseObject>() is Player && hit.transform.gameObject.GetComponent<BaseObject>().isAlly)
                        {
                            SendDataTCP.SendDamage(
                                                    ObjectType.player,
                                                    hit.transform.gameObject.GetComponent<BaseObject>().index,
                                                    PhysicDamage,
                                                    IgnisDamage,
                                                    TerraDamage,
                                                    AquaDamage,
                                                    CaeliDamage,
                                                    PureDamage
                                                   );
                        }
                        if (hit.transform.gameObject.GetComponent<BaseObject>() is Spell && hit.transform.gameObject.GetComponent<BaseObject>().isAlly)
                        {
                            SendDataTCP.SendDamage(
                                                    ObjectType.spell,
                                                    hit.transform.gameObject.GetComponent<BaseObject>().index,
                                                    PhysicDamage,
                                                    IgnisDamage,
                                                    TerraDamage,
                                                    AquaDamage,
                                                    CaeliDamage,
                                                    PureDamage
                                                   );
                        }
                    }
                    if (DeathAfterDamage)
                    {
                        SendDataTCP.SendDestroy(ObjectType.spell, Spell.index);
                    }
                }
            }
            if (Serialize == DamageSerialize.Single)
            {
                if (collision.gameObject.GetComponent<BaseObject>() != null)
                {
                    if (collision.gameObject.GetComponent<BaseObject>() is Player)
                    {
                        SendDataTCP.SendDamage(
                                                ObjectType.player,
                                                collision.gameObject.GetComponent<BaseObject>().index,
                                                PhysicDamage,
                                                IgnisDamage,
                                                TerraDamage,
                                                AquaDamage,
                                                CaeliDamage,
                                                PureDamage
                                               );
                    }
                    if (collision.gameObject.GetComponent<BaseObject>() is Spell)
                    {
                        SendDataTCP.SendDamage(
                                                ObjectType.spell,
                                                collision.gameObject.GetComponent<BaseObject>().index,
                                                PhysicDamage,
                                                IgnisDamage,
                                                TerraDamage,
                                                AquaDamage,
                                                CaeliDamage,
                                                PureDamage
                                               );
                    }
                }
                if (DeathAfterDamage)
                {
                    SendDataTCP.SendDestroy(ObjectType.spell, Spell.index);
                }
            }
        }

        if(Targets == DamageTargets.All)
        {
            if (Serialize == DamageSerialize.Area)
            {
                RaycastHit[] hits;
                hits = Physics.SphereCastAll(transform.position, Spell.Stats.CastArea, Vector3.up);
                foreach (RaycastHit hit in hits)
                {
                    if (hit.transform.gameObject.GetComponent<BaseObject>() != null)
                    {
                        if (hit.transform.gameObject.GetComponent<BaseObject>() is Player)
                        {
                            SendDataTCP.SendDamage(
                                                    ObjectType.player,
                                                    hit.transform.gameObject.GetComponent<BaseObject>().index,
                                                    PhysicDamage,
                                                    IgnisDamage,
                                                    TerraDamage,
                                                    AquaDamage,
                                                    CaeliDamage,
                                                    PureDamage
                                                   );
                        }
                        if (hit.transform.gameObject.GetComponent<BaseObject>() is Spell)
                        {
                            SendDataTCP.SendDamage(
                                                    ObjectType.spell,
                                                    hit.transform.gameObject.GetComponent<BaseObject>().index,
                                                    PhysicDamage,
                                                    IgnisDamage,
                                                    TerraDamage,
                                                    AquaDamage,
                                                    CaeliDamage,
                                                    PureDamage
                                                   );
                        }
                    }
                    if (DeathAfterDamage)
                    {
                        SendDataTCP.SendDestroy(ObjectType.spell, Spell.index);
                    }
                }
            }
            if (Serialize == DamageSerialize.Single)
            {
                if (collision.gameObject.GetComponent<BaseObject>() != null)
                {
                    if (collision.gameObject.GetComponent<BaseObject>() is Player)
                    {
                        SendDataTCP.SendDamage(
                                                ObjectType.player,
                                                collision.gameObject.GetComponent<BaseObject>().index,
                                                PhysicDamage,
                                                IgnisDamage,
                                                TerraDamage,
                                                AquaDamage,
                                                CaeliDamage,
                                                PureDamage
                                               );
                    }
                    if (collision.gameObject.GetComponent<BaseObject>() is Spell)
                    {
                        SendDataTCP.SendDamage(
                                                ObjectType.spell,
                                                collision.gameObject.GetComponent<BaseObject>().index,
                                                PhysicDamage,
                                                IgnisDamage,
                                                TerraDamage,
                                                AquaDamage,
                                                CaeliDamage,
                                                PureDamage
                                               );
                    }
                }
                if (DeathAfterDamage)
                {
                    SendDataTCP.SendDestroy(ObjectType.spell, Spell.index);
                }
            }
        }
    }
}
