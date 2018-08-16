using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkObjectController : MonoBehaviour {

    #region Variables

    public int index;
    public int hp;
    public bool owner;

    private Rigidbody _thisRigitbody;
    public SpellProperties spell;
    public PropStats propStats;

    #endregion

    public void CheckPosition(int UpdateIndex, float[] pos)
    {
        
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}

