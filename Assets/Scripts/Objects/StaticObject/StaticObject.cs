public class StaticObject : BaseObject
{

    public virtual void Init(int hp, int index)
    {
        base.Init(index, ObjectType.staticObject);
        initiaziled = true;
        hpUpdate.Init(hp, this);
    }
}
