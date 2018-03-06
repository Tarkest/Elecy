﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

    private Vector3 _spawnPos;
    private Quaternion _spawnRot;

    public void SpawnPlayer()
    {
        _spawnPos = gameObject.transform.position;
        _spawnRot = gameObject.transform.rotation;

        Instantiate(Resources.Load("/Players/TestPlayer"), _spawnPos, _spawnRot);
        Destroy(gameObject);
    }

    public void SpawnDummy()
    {
        _spawnPos = gameObject.transform.position;
        _spawnRot = gameObject.transform.rotation;

        Instantiate(Resources.Load("/Players/dummy"), _spawnPos, _spawnRot);
        Destroy(gameObject);

    }

    public Vector3 ReturnTransform()
    {
        return _spawnPos;
    }

    public Quaternion ReturnRotation()
    {
        return _spawnRot;
    }
}
