using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleLoader : MonoBehaviour {

    private static ObjectManager _thisLoadedmanager;

    #region LoadScreen
    private static GameObject _loadScreen;
    private static Slider _yourLoadProgress;
    private static Slider _enemyLoadProgress;
    private static float _thisPlayerProgress = 0f;
    private static float _enemyPlayerProgress = 0f;
    private static int _loadStages = 0;
    #endregion

    void Awake()
    {
        _loadScreen = GameObject.Find("LoadScreen");
        _yourLoadProgress = _loadScreen.transform.Find("ThisPlayerLoad").GetComponent<Slider>();
        _enemyLoadProgress = _loadScreen.transform.Find("EnemyPlayerLoad").GetComponent<Slider>();
    }

    void Start()
    {
        ClientTCP.EnterRoom();    
    }

    void Update()
    {
        _yourLoadProgress.value = _thisPlayerProgress;
        _enemyLoadProgress.value = _enemyPlayerProgress;
    }

    public static void LoadScene(int MapIndex)
    {
        MainThread.executeInUpdate(() => Instantiate(Resources.Load("Maps/" + MapIndex.ToString() + "/GameArea"), Vector3.zero, Quaternion.identity));
    }

    public static void SceneLoaded(ObjectManager LoadedManager)
    {
        _thisLoadedmanager = LoadedManager;
        _loadStages += 3;
        if (_thisLoadedmanager.bigRocksPrefab.Length > 0 || _thisLoadedmanager.middleRocksPrefab.Length > 0 || _thisLoadedmanager.smallRocksPrefab.Length > 0)
            _loadStages += 1;
        if (_thisLoadedmanager.bigTreesPrefab.Length > 0 || _thisLoadedmanager.middleTreesPrefab.Length > 0 || _thisLoadedmanager.smallTreesPrefab.Length > 0)
            _loadStages += 1;
        ThisPlayerProgressChange(1f / _loadStages);
        SendDataTCP.SendBeginLoading(_thisPlayerProgress);
    }

    private static void CheckForLoadingRocks()
    {
        if(_thisLoadedmanager.bigRocksPrefab.Length > 0 || _thisLoadedmanager.middleRocksPrefab.Length > 0 || _thisLoadedmanager.smallRocksPrefab.Length > 0)
        {
            int _rocksTypeCount = _thisLoadedmanager.rocksCount;
            bool _bigRocks = _thisLoadedmanager.bigRocksPrefab.Length > 0;
            bool _middleRocks = _thisLoadedmanager.middleRocksPrefab.Length > 0;
            bool _smallRocks = _thisLoadedmanager.smallRocksPrefab.Length > 0;
            SendDataTCP.SendSpawnRocks(_thisPlayerProgress, _rocksTypeCount, _bigRocks, _middleRocks, _smallRocks);
        }
        else
        {
            CheckForLoadingTrees();
        }
    }

    private static void CheckForLoadingTrees()
    {
        if(_thisLoadedmanager.bigTreesPrefab.Length > 0 || _thisLoadedmanager.middleTreesPrefab.Length > 0 || _thisLoadedmanager.smallTreesPrefab.Length > 0)
        {
            int _treesTypeCount = _thisLoadedmanager.rocksCount;
            bool _bigTrees = _thisLoadedmanager.bigRocksPrefab.Length > 0;
            bool _middleTrees = _thisLoadedmanager.middleRocksPrefab.Length > 0;
            bool _smallTrees = _thisLoadedmanager.smallRocksPrefab.Length > 0;
            SendDataTCP.SendSpawnTrees(_thisPlayerProgress, _treesTypeCount, _bigTrees, _middleTrees, _smallTrees);
        }
        else
        {
            SendDataTCP.SendGetSpells(_thisPlayerProgress);
        }
    }

    #region Procesing
    public static void SpanwPlayers(string nickname1, string nickname2, float[][] positions, float[][] rotations)
    {
        DeveloperScreenController.AddInfo("Begin Load: Players", 1);
        if (nickname1 == ClientTCP.nickname)
        {
            _thisLoadedmanager.SetStartTransform(positions[0], positions[1], rotations[0], rotations[1]);
            GameObject _player = Instantiate(Resources.Load("Players/Player"), _thisLoadedmanager.GetPlayerStartPosition(), _thisLoadedmanager.GetPlayerStartRotation()) as GameObject;
            _player.GetComponent<PlayerStats>().SetStats(1000, 1000, 10f, 10f, 10, 5, 5, 5, 5);
            ObjectManager.players[0] = _player;
            DeveloperScreenController.AddInfo("Player Load...OK", 1);
            ThisPlayerProgressChange(_thisPlayerProgress + (1f / _loadStages) / 2);
            GameObject _enemy = Instantiate(Resources.Load("Players/Player"), _thisLoadedmanager.GetEnemyStartPosition(), _thisLoadedmanager.GetEnemyStartRotation()) as GameObject;
            _enemy.GetComponent<PlayerStats>().SetStats(1000, 1000, 10f, 10f, 10, 5, 5, 5, 5);
            ObjectManager.players[1] = _enemy;
            DeveloperScreenController.AddInfo("Enemy Load...OK", 1);
            ThisPlayerProgressChange(_thisPlayerProgress + (1f / _loadStages) / 2);
        }
        else
        {
            _thisLoadedmanager.SetStartTransform(positions[1], positions[0], rotations[1], rotations[0]);
            GameObject _player = Instantiate(Resources.Load("Players/Player"), _thisLoadedmanager.GetPlayerStartPosition(), _thisLoadedmanager.GetPlayerStartRotation()) as GameObject;
            _player.GetComponent<PlayerStats>().SetStats(1000, 1000, 10f, 10f, 10, 5, 5, 5, 5);
            ObjectManager.players[1] = _player;
            DeveloperScreenController.AddInfo("Player Load...OK", 1);
            ThisPlayerProgressChange(_thisPlayerProgress + (1f / _loadStages) / 2);
            GameObject _enemy = Instantiate(Resources.Load("Players/Player"), _thisLoadedmanager.GetEnemyStartPosition(), _thisLoadedmanager.GetEnemyStartRotation()) as GameObject;
            _enemy.GetComponent<PlayerStats>().SetStats(1000, 1000, 10f, 10f, 10, 5, 5, 5, 5);
            ObjectManager.players[0] = _enemy;
            DeveloperScreenController.AddInfo("Enemy Load...OK", 1);
            ThisPlayerProgressChange(_thisPlayerProgress + (1f / _loadStages) / 2);
        }
        CheckForLoadingRocks();

    }

    public static void SpawnRocks(int rocksCount, int[] index ,int[] rocksHP, float[][] rocksPosition, float[][] rocksRotation)
    {
        DeveloperScreenController.AddInfo("Begin Load: Rocks", 1);
        DeveloperScreenController.AddInfo("Rock Count: " + rocksCount.ToString(), 1);
        GameObject _rocks = new GameObject("Rocks");
        _rocks.transform.parent = _thisLoadedmanager.gameObject.transform;
        GameObject NewRock = null;
        int small = 0;
        int middle = 0;
        int big = 0;
        for (int i = 0; i < rocksCount; i++)
        {
            if(rocksHP[i] < 20)
            {
                if(_thisLoadedmanager.smallRocksPrefab.Length > 1)
                {
                    NewRock = Instantiate(_thisLoadedmanager.smallRocksPrefab[small], new Vector3(rocksPosition[i][0], rocksPosition[i][1], rocksPosition[i][2]), new Quaternion(rocksRotation[i][0], rocksRotation[i][1], rocksRotation[i][2], rocksRotation[i][3]), _rocks.transform);
                    small++;
                    if (small > _thisLoadedmanager.smallRocksPrefab.Length)
                        small = 0;
                }
                else
                {
                    NewRock = Instantiate(_thisLoadedmanager.smallRocksPrefab[0], new Vector3(rocksPosition[i][0], rocksPosition[i][1], rocksPosition[i][2]), new Quaternion(rocksRotation[i][0], rocksRotation[i][1], rocksRotation[i][2], rocksRotation[i][3]), _rocks.transform);
                }
            }
            else if(rocksHP[i] > 20 && rocksHP[i] < 30)
            {
                if (_thisLoadedmanager.middleRocksPrefab.Length > 1)
                {
                    NewRock = Instantiate(_thisLoadedmanager.middleRocksPrefab[middle], new Vector3(rocksPosition[i][0], rocksPosition[i][1], rocksPosition[i][2]), new Quaternion(rocksRotation[i][0], rocksRotation[i][1], rocksRotation[i][2], rocksRotation[i][3]), _rocks.transform);
                    middle++;
                    if (middle > _thisLoadedmanager.middleRocksPrefab.Length)
                        middle = 0;
                }
                else
                {
                    NewRock = Instantiate(_thisLoadedmanager.middleRocksPrefab[0], new Vector3(rocksPosition[i][0], rocksPosition[i][1], rocksPosition[i][2]), new Quaternion(rocksRotation[i][0], rocksRotation[i][1], rocksRotation[i][2], rocksRotation[i][3]), _rocks.transform);
                }
            }
            else if(rocksHP[i] > 30)
            {
                if (_thisLoadedmanager.bigRocksPrefab.Length > 1)
                {
                    NewRock = Instantiate(_thisLoadedmanager.bigRocksPrefab[big], new Vector3(rocksPosition[i][0], rocksPosition[i][1], rocksPosition[i][2]), new Quaternion(rocksRotation[i][0], rocksRotation[i][1], rocksRotation[i][2], rocksRotation[i][3]), _rocks.transform);
                    big++;
                    if (big > _thisLoadedmanager.bigRocksPrefab.Length)
                        big = 0;
                }
                else
                {
                    NewRock = Instantiate(_thisLoadedmanager.bigRocksPrefab[0], new Vector3(rocksPosition[i][0], rocksPosition[i][1], rocksPosition[i][2]), new Quaternion(rocksRotation[i][0], rocksRotation[i][1], rocksRotation[i][2], rocksRotation[i][3]), _rocks.transform);
                }
            }
            if(NewRock != null)
            {
                ObjectManager.staticPropsList.Add(NewRock);
            }
            ThisPlayerProgressChange(_thisPlayerProgress + (1f / _loadStages) / rocksCount);
        }
        DeveloperScreenController.AddInfo("Rock Load...OK", 1);
        CheckForLoadingTrees();
    }

    public static void SpawnTrees(int treesCount, int[] index, int[] treesHP, float[][] treesPosition, float[][] treesRotation)
    {
        DeveloperScreenController.AddInfo("Begin Load: Trees", 1);
        DeveloperScreenController.AddInfo("Trees Count: " + treesCount.ToString(), 1);
        GameObject _trees = new GameObject("Trees");
        _trees.transform.parent = _thisLoadedmanager.gameObject.transform;
        GameObject NewTree = null;
        int small = 0;
        int middle = 0;
        int big = 0;
        for (int i = 0; i < treesCount; i++)
        {
            if (treesHP[i] < 20)
            {
                if (_thisLoadedmanager.smallTreesPrefab.Length > 1)
                {
                    NewTree = Instantiate(_thisLoadedmanager.smallTreesPrefab[small], new Vector3(treesPosition[i][0], treesPosition[i][1], treesPosition[i][2]), new Quaternion(treesRotation[i][0], treesRotation[i][1], treesRotation[i][2], treesRotation[i][3]), _trees.transform);
                    small++;
                    if (small > _thisLoadedmanager.smallTreesPrefab.Length)
                        small = 0;
                }
                else
                {
                    NewTree = Instantiate(_thisLoadedmanager.smallTreesPrefab[0], new Vector3(treesPosition[i][0], treesPosition[i][1], treesPosition[i][2]), new Quaternion(treesRotation[i][0], treesRotation[i][1], treesRotation[i][2], treesRotation[i][3]), _trees.transform);
                }
            }
            else if (treesHP[i] > 20 && treesHP[i] < 30)
            {
                if (_thisLoadedmanager.middleTreesPrefab.Length > 1)
                {
                    NewTree = Instantiate(_thisLoadedmanager.middleTreesPrefab[middle], new Vector3(treesPosition[i][0], treesPosition[i][1], treesPosition[i][2]), new Quaternion(treesRotation[i][0], treesRotation[i][1], treesRotation[i][2], treesRotation[i][3]), _trees.transform);
                    middle++;
                    if (middle > _thisLoadedmanager.middleTreesPrefab.Length)
                        middle = 0;
                }
                else
                {
                    NewTree = Instantiate(_thisLoadedmanager.middleTreesPrefab[0], new Vector3(treesPosition[i][0], treesPosition[i][1], treesPosition[i][2]), new Quaternion(treesRotation[i][0], treesRotation[i][1], treesRotation[i][2], treesRotation[i][3]), _trees.transform);
                }
            }
            else if (treesHP[i] > 30)
            {
                if (_thisLoadedmanager.bigTreesPrefab.Length > 1)
                {
                    NewTree = Instantiate(_thisLoadedmanager.bigTreesPrefab[big], new Vector3(treesPosition[i][0], treesPosition[i][1], treesPosition[i][2]), new Quaternion(treesRotation[i][0], treesRotation[i][1], treesRotation[i][2], treesRotation[i][3]), _trees.transform);
                    big++;
                    if (big > _thisLoadedmanager.bigTreesPrefab.Length)
                        big = 0;
                }
                else
                {
                    NewTree = Instantiate(_thisLoadedmanager.bigTreesPrefab[0], new Vector3(treesPosition[i][0], treesPosition[i][1], treesPosition[i][2]), new Quaternion(treesRotation[i][0], treesRotation[i][1], treesRotation[i][2], treesRotation[i][3]), _trees.transform);
                }
            }
            if(NewTree != null)
            {
                ObjectManager.staticPropsList.Add(NewTree);
            }
            ThisPlayerProgressChange(_thisPlayerProgress + (1f / _loadStages) / treesCount);
        }
        DeveloperScreenController.AddInfo("Trees Load...OK", 1);
        SendDataTCP.SendGetSpells(_thisPlayerProgress);
    }

    public static void LoadSpells(int[] SpellsIndexes)
    {
        DeveloperScreenController.AddInfo("Begin Load: Spells", 1);
        DeveloperScreenController.AddInfo("Speels Count: " + SpellsIndexes.Length.ToString(), 1);
        DeveloperScreenController.AddInfo("Spells: ", 1);
        for (int i = 0; i < SpellsIndexes.Length; i++)
        {
            DeveloperScreenController.AddInfo(i.ToString() + ": " + SpellsIndexes[i].ToString(), 1);
            ThisPlayerProgressChange(_thisPlayerProgress + (1f / _loadStages) / SpellsIndexes.Length);
        }
        DeveloperScreenController.AddInfo("Spells Load...OK", 1);
    }

    public static void LoadComplite()
    {
        SendDataTCP.SendLoadComplite(_thisPlayerProgress);
    }
    #endregion

    #region LoadScreen

    public static void EnemyProgressChange(float progress)
    {
        _enemyPlayerProgress = progress;
    }

    public static void ThisPlayerProgressChange(float progress)
    {
        _thisPlayerProgress = progress;
    }
    #endregion

    public static void StartBattle()
    {
        MainThread.executeInUpdate(() => _loadScreen.SetActive(false));
        BattleLogic.BeginBattle();
    }
}
