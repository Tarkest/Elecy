using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

    private Vector3 _spawnPos;
    private Quaternion _spawnRot;
    private bool spawnPlayer = false;
    private bool spawnEnemy = false;


    private void Awake()
    {
        _spawnPos = gameObject.transform.position;
        _spawnRot = gameObject.transform.rotation;
    }

    private void Update()
    {
        if (spawnPlayer)
        {
            spawnPlayer = false;
            GameObject.Find("Test player").GetComponent<Transform>().position = _spawnPos;
            GameObject.Find("Test player").GetComponent<Transform>().rotation = _spawnRot;
            Destroy(gameObject);
        }
        if (spawnEnemy)
        {
            spawnEnemy = false;
            GameObject.Find("dummy").GetComponent<Transform>().position = _spawnPos;
            GameObject.Find("dummy").GetComponent<Transform>().rotation = _spawnRot;
            Destroy(gameObject);
        }
    }
    public void SpawnPlayer()
    {
        spawnPlayer = true;
    }

    public void SpawnDummy()
    {
        spawnEnemy = true;
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
