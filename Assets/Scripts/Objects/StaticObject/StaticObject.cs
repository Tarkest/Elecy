public class StaticObject : BaseObject
{

    public virtual void Init(int hp, int index)
    {
        base.Init(index, ObjectType.staticObject);
        initiaziled = true;
        hpUpdate.Init(hp, this);
    }

    public override void GetDamage(int PhysicDamage, int IgnisDamage, int TerraDamage, int CaeliDamage, int AquaDamage, int PureDamage, bool heal = false)
    {

    }
}
