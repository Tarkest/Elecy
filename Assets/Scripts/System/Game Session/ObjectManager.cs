using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour {

    #region Prefabs

    public GameObject bigTreesPrefab;
    public GameObject middleTreesPrefab;
    public GameObject smallTreesPrefab;

    public GameObject bigRocksPrefab;
    public GameObject middleRocksPrefab;
    public GameObject smallRocksPrefab;

    #endregion

    #region ObjectsContainer

    [System.NonSerialized]
    public GameObject[] players;

    public List<GameObject> dynamicPropList = new List<GameObject>();

    public List<GameObject> staticPropsList = new List<GameObject>();

    #endregion

    #region Variables

    public bool _isRocksExist;
    public bool _isTreesExist;

    #endregion

    void Start()
    {
        
    }
}
