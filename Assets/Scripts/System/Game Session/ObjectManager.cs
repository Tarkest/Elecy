using System;
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

    public static CameraProperties cameraTarger;

    #endregion

    #region ObjectsContainer

    public Player[] Players;

    public NetworkObjectList dynamicPropList = new NetworkObjectList();

    public List<GameObject> staticPropsList = new List<GameObject>();

    public Dictionary<int, GameObject[]> spells = new Dictionary<int, GameObject[]>();

    #endregion

    #region Variables

    public int treesCount;
    public int rocksCount;

    public static int playerIndex;

    #endregion

    #region Reusable Variables
    private Vector3[] startPositions;
    private Quaternion[] startRotations;

    private Vector3 _playerStartPosition;
    private Vector3 _enemyStartPosition;
    private Quaternion _playerStartRotation;
    private Quaternion _enemyStartRotation;
    #endregion

    void Start()
    {
        BattleLoader.SceneLoaded(this);
        Network.SetManager(this);
    }

    public void UpdatePrefabs()
    {
        for (int i = 0; i < dynamicPropList.Lenght(); i++)
        {
            if(dynamicPropList[i] != null)
                dynamicPropList[i].Callback();
        }
    }

    #region Get & Set

    public void SetStartTransform(float[] pos1, float[] pos2, float[] rot1, float[] rot2)
    {
        _playerStartPosition = new Vector3(pos1[0], pos1[1], pos1[2]);
        _enemyStartPosition = new Vector3(pos2[0], pos2[1], pos2[2]);
        _playerStartRotation = new Quaternion(rot1[0], rot1[1], rot1[2], rot1[3]);
        _enemyStartRotation = new Quaternion(rot2[0], rot2[1], rot2[2], rot2[3]);
    }

    public void SetStartProperties(int playersCount, float[][] pos, float[][] rot)
    {
        Players = new Player[playersCount];
        startPositions = new Vector3[playersCount];
        startRotations = new Quaternion[playersCount];
        for(int i = 0; i < playersCount; i++)
        {
            startPositions[i] = new Vector3(pos[i][0], pos[i][1], pos[i][2]);
            startRotations[i] = new Quaternion(rot[i][0], rot[i][1], rot[i][2], rot[i][3]);
        }
    }

    public Vector3 GetStartPosition(int index)
    {
        return startPositions[index];
    }

    public Quaternion GetStartRotation(int index)
    {
        return startRotations[index];
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

public class NetworkObjectList
{
    Dictionary<int, Spell> List;

    public NetworkObjectList()
    {
        List = new Dictionary<int, Spell>();
    }

    public void Add(Spell Object, int index)
    {
        lock(List)
        {
            List.Add(index, Object);
            Object.index = index;
        }

    }

    public Spell this[int index]
    {
        get
        {
            return List[index];
        }
    }

    public int Lenght()
    {
        return List.Count;
    }

    public void Remove(int index)
    {
        lock(List)
        {
            List[index].Destroy();
            List.Remove(index);
        }
    }
}
