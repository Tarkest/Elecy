﻿using UnityEngine;

public class TestSpell : BaseObject
{

    public Spell spell;
    public Spell dummy;
    public new BaseMovement Movement { get { return spell.Movement; } }
    public new SpellStats Stats { get { return spell.Stats as SpellStats; } }
    public new int index { get { return spell.index; } }

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
        spell.SetStartProperties(castPosition, targetPosition, index, isMain);
        dummy.SetStartProperties(castPosition, targetPosition, index);
    }

    protected internal override void CheckPosition(int index, float[] pos)
    {
        spell.Movement.CheckPosition(index, pos);
        dummy.Movement.CheckPosition(index, pos);
    }

    protected internal override void Move()
    {
        spell.Movement.Move();
    }



    protected void SetProtected(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject _dummy = Instantiate(prefab, position, rotation, this.transform) as GameObject;
        dummy = _dummy.GetComponent<Spell>();
        dummyVisibility = _dummy.GetComponentInChildren<MeshRenderer>();
        GameObject _spell = Instantiate(prefab, position, rotation, this.transform) as GameObject;
        spell = _spell.GetComponent<Spell>();
        spellVisibility = _spell.GetComponentInChildren<MeshRenderer>();

    }

    #region Not Implementet (no use)

    protected internal override void SetBaseStats() { }

    protected internal override void SetMovement(bool isPlayer = false, params Vector3[] pos) { }

    #endregion

}

