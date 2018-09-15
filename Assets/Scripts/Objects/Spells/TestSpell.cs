using UnityEngine;

public class TestSpell : Spell, ITest<Spell>
{

    internal new BaseMovement Movement { get { return mObject.Movement; } }
    internal new SpellStats Stats { get { return mObject.Stats as SpellStats; } }
    internal new int index { get { return mObject.index; } }

    public Spell mObject
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
    public Spell Dummy
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

    protected MeshRenderer spellVisibility;
    protected MeshRenderer dummyVisibility;

    private void Update()
    {
        if((Network.currentManager.Players[ObjectManager.playerIndex] as TestPlayer).Player)
        {
            spellVisibility.enabled = true;
            dummyVisibility.enabled = false;
        }
        else
        {
            spellVisibility.enabled = false;
            dummyVisibility.enabled = true;
        }
    }

    internal void SetStartProperties(GameObject prefab, Vector3 castPosition, Quaternion rotation, Vector3 targetPosition, int index, bool isMain = false)
    {
        SetProtected(prefab, castPosition, rotation);
        mObject.Init(castPosition, targetPosition, index, isMain);
        Dummy.Init(castPosition, targetPosition, index);
    }

    protected void SetProtected(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject _dummy = Instantiate(prefab, position, rotation, this.transform) as GameObject;
        Dummy = _dummy.GetComponent<Spell>();
        dummyVisibility = _dummy.GetComponentInChildren<MeshRenderer>();
        _dummy.tag = Tags.Spell.ToString();
        GameObject _spell = Instantiate(prefab, position, rotation, this.transform) as GameObject;
        mObject = _spell.GetComponent<Spell>();
        spellVisibility = _spell.GetComponentInChildren<MeshRenderer>();
        _spell.tag = Tags.Spell.ToString();
    }

}

