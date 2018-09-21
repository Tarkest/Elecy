
public class TestStaticObject : StaticObject, ITest<StaticObject>
{

    public StaticObject mObject
    {
        get
        {
            return mObject;
        }
        private set
        {
            mObject = value;
        }
    }
    public StaticObject Dummy
    {
        get
        {
            return Dummy;
        }
        private set
        {
            Dummy = value;
        }
    }

    public override void Init(int hp)
    {
        SetProtected();
        mObject.Init(hp);
        Dummy.Init(hp);
    }

    #region Overrided Test Commands

    public override void Callback()
    {
        mObject.Callback();
    }

    public override void CheckPosition(int index, float[] pos)
    {
        mObject.CheckPosition(index, pos);
        Dummy.CheckPosition(index, pos);
    }

    public override void HPOuterChange(int change)
    {
        mObject.HPOuterChange(change);
        Dummy.HPOuterChange(change);
    }

    #endregion

    protected void SetProtected() // in next episod
    {
        //GameObject _dummy = Instantiate(prefab, position, rotation, this.transform) as GameObject;
        //Dummy = _dummy.GetComponent<Spell>();
        //dummyVisibility = _dummy.GetComponentInChildren<MeshRenderer>();
        //_dummy.tag = Tags.Spell.ToString();
        //GameObject _spell = Instantiate(prefab, position, rotation, this.transform) as GameObject;
        //mObject = _spell.GetComponent<Spell>();
        //spellVisibility = _spell.GetComponentInChildren<MeshRenderer>();
        //_spell.tag = Tags.Spell.ToString();
    }
}

