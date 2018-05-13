using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectNetworkListener : MonoBehaviour {

    private Rigidbody _thisObjectRigitbody;
    private DynamicProp _thisObjectDynProp;

    void Awake()
    {
        _thisObjectRigitbody = gameObject.GetComponent<Rigidbody>();
        _thisObjectDynProp = gameObject.GetComponent<DynamicProp>();
    }

    void Update ()
    {
        float _posDistance = Vector3.Distance(_thisObjectDynProp.GetPosition(), _thisObjectDynProp.GetServerPosition());
        if(_posDistance > 0.1f && _posDistance < 10f)
        {
            _thisObjectRigitbody.MovePosition(Vector3.Lerp(_thisObjectDynProp.GetPosition(), _thisObjectDynProp.GetServerPosition(), 0.1f)); 
        }
        else
        {
            _thisObjectRigitbody.MovePosition(_thisObjectDynProp.GetServerPosition());
        }
        _thisObjectRigitbody.MoveRotation(Quaternion.Lerp(_thisObjectDynProp.GetRotation(), _thisObjectDynProp.GetServerRotation(), 0.1f));
	}

    public void RemoveComponent()
    {
        Destroy(this);
    }
}
