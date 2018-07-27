using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour {

    #region Prefabs

    public GameObject[] bigTreesPrefab;
    public GameObject[] middleTreesPrefab;
    public GameObject[] smallTreesPrefab;

    public GameObject[] bigRocksPrefab;
    public GameObject[] middleRocksPrefab;
    public GameObject[] smallRocksPrefab;

    #endregion

    #region ObjectsContainer

    public GameObject[] players = new GameObject[2];

    public List<GameObject> dynamicPropList = new List<GameObject>();

    public List<GameObject> staticPropsList = new List<GameObject>();

    #endregion

    #region Variables

    public int treesCount;
    public int rocksCount;

    #endregion

    #region Reusable Variables
    private Vector3 _playerStartPosition;
    private Vector3 _enemyStartPosition;
    private Quaternion _playerStartRotation;
    private Quaternion _enemyStartRotation;
    #endregion

    void Start()
    {
        BattleLoader.SceneLoaded(this);
    }

    #region Get & Set

    public void SetStartTransform(float[] pos1, float[] pos2, float[] rot1, float[] rot2)
    {
        _playerStartPosition = new Vector3(pos1[0], pos1[1], pos1[2]);
        _enemyStartPosition = new Vector3(pos2[0], pos2[1], pos2[2]);
        _playerStartRotation = new Quaternion(rot1[0], rot1[1], rot1[2], rot1[3]);
        _enemyStartRotation = new Quaternion(rot2[0], rot2[1], rot2[2], rot2[3]);
    }


    public Vector3 GetPlayerStartPosition()
    {
        return _playerStartPosition;
    }

    public Vector3 GetEnemyStartPosition()
    {
        return _enemyStartPosition;
    }

    public Quaternion GetPlayerStartRotation()
    {
        return _playerStartRotation;
    }

    public Quaternion GetEnemyStartRotation()
    {
        return _enemyStartRotation;
    }

    #endregion
}
