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

    public static GameObject[] players;

    public static List<GameObject> dynamicPropList = new List<GameObject>();

    public static List<GameObject> staticPropsList = new List<GameObject>();

    #endregion

    #region Variables

    public int treesCount;
    public int rocksCount;

    #endregion

    void Start()
    {
        BattleLoader.SceneLoaded(this);
    }
}
