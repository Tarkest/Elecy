using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class BattleLoader : MonoBehaviour {

    #region Private Members

    private static ObjectManager _thisLoadedmanager;
    private static string  _race;

    #region LoadScreen
    private static GameObject _loadScreen;
    private static Slider _yourLoadProgress;
    private static Slider _enemyLoadProgress;
    private static float _thisPlayerProgress = 0f;
    private static float _enemyPlayerProgress = 0f;
    private static int _loadStages = 0;
    #endregion

    #endregion

    #region Unity's Methods

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

    #endregion

    public static void LoadScene(int MapIndex)
    {
        MainThread.executeInUpdate(() => { Instantiate(Resources.Load("Maps/" + MapIndex.ToString() + "/GameArea"), Vector3.zero, Quaternion.identity);
            DeveloperScreenController.AddInfo("Map Loaded", 1);
            SetRace(Network.currentRace);
            Network.currentRace = null;
        });
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

    public static void SetRace(string race)
    {
        _race = race;
    }

    #region Private Methods

    private static void CheckForLoadingRocks()
    {
        if ((_thisLoadedmanager.bigRocksPrefab.Length > 0 || _thisLoadedmanager.middleRocksPrefab.Length > 0 || _thisLoadedmanager.smallRocksPrefab.Length > 0) && _thisLoadedmanager.rocksCount > 0)
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
        if ((_thisLoadedmanager.bigTreesPrefab.Length > 0 || _thisLoadedmanager.middleTreesPrefab.Length > 0 || _thisLoadedmanager.smallTreesPrefab.Length > 0) && _thisLoadedmanager.treesCount > 0)
        {
            int _treesTypeCount = _thisLoadedmanager.treesCount;
            bool _bigTrees = _thisLoadedmanager.bigTreesPrefab.Length > 0;
            bool _middleTrees = _thisLoadedmanager.middleTreesPrefab.Length > 0;
            bool _smallTrees = _thisLoadedmanager.smallTreesPrefab.Length > 0;
            SendDataTCP.SendSpawnTrees(_thisPlayerProgress, _treesTypeCount, _bigTrees, _middleTrees, _smallTrees);
        }
        else
        {
            SendDataTCP.SendGetSpells(_thisPlayerProgress);
        }
    }

    #endregion

    #region Procesing

    public static void SpanwPlayers(string[] nicknames, float[][] positions, float[][] rotations, int playersCount)
    {

        DeveloperScreenController.AddInfo("Begin Load: Players", 1);
        // Creates arrays of Position / Rotation / Players
        _thisLoadedmanager.SetStartProperties(playersCount, positions, rotations);
        ObjectManager.cameraTarger = GameObject.Find("Main Camera").GetComponent<CameraProperties>();
        // in game mode Spawns 'playersCount' - amount of players
        for (int i = 0; i < playersCount; i++)
        {
            // if nickname equals to client's nickname - spawns Player
            if (nicknames[i] == ClientTCP.nickname)
            {
                SpawnPlayer(nicknames[i], positions[i], rotations[i], i);
            }
            else
                // or spawns Enemy
                SpawnEnemy(nicknames[i], positions[i], rotations[i], i);
        }

        ThisPlayerProgressChange(_thisPlayerProgress + (1f / _loadStages) / 2);

        CheckForLoadingRocks();
    }

    public static void SpawnRocks(int rocksCount, int[] index, int[] rocksHP, float[][] rocksPosition, float[][] rocksRotation)
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
            GameObject prefab = null;

            #region Prefab choose

            if (rocksHP[i] <= 20)
            {
                if (_thisLoadedmanager.smallRocksPrefab.Length > 1)
                {
                    prefab = _thisLoadedmanager.smallRocksPrefab[small];
                    small++;
                    if (small > _thisLoadedmanager.smallRocksPrefab.Length)
                        small = 0;
                }
                else
                {
                    prefab = _thisLoadedmanager.smallRocksPrefab[0];
                }
            }
            else if (rocksHP[i] > 20 && rocksHP[i] < 30)
            {
                if (_thisLoadedmanager.middleRocksPrefab.Length > 1)
                {
                    prefab = _thisLoadedmanager.middleRocksPrefab[middle];
                    middle++;
                    if (middle > _thisLoadedmanager.middleRocksPrefab.Length)
                        middle = 0;
                }
                else
                {
                    prefab = _thisLoadedmanager.middleRocksPrefab[0];
                }
            }
            else if (rocksHP[i] >= 30)
            {
                if (_thisLoadedmanager.bigRocksPrefab.Length > 1)
                {
                    prefab = _thisLoadedmanager.bigRocksPrefab[big];
                    big++;
                    if (big > _thisLoadedmanager.bigRocksPrefab.Length)
                        big = 0;
                }
                else
                {
                    prefab = _thisLoadedmanager.bigRocksPrefab[0];
                }
            }

            #endregion

            if (prefab != null)
            {
                NewRock = Instantiate(prefab, new Vector3(rocksPosition[i][0], rocksPosition[i][1], rocksPosition[i][2]), new Quaternion(rocksRotation[i][0], rocksRotation[i][1], rocksRotation[i][2], rocksRotation[i][3]), _rocks.transform);
                if (NewRock != null)
                {
                    NewRock.tag = Tags.StaticObject.ToString();
                    _thisLoadedmanager.staticPropsList.Add(NewRock);
                    NewRock = null;
                }
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
            GameObject prefab = null;

            #region Prefab choose

            if (treesHP[i] <= 20)
            {
                if (_thisLoadedmanager.smallTreesPrefab.Length > 1)
                {
                    prefab = _thisLoadedmanager.smallTreesPrefab[small];
                    small++;
                    if (small > _thisLoadedmanager.smallTreesPrefab.Length)
                        small = 0;
                }
                else
                {
                    prefab = _thisLoadedmanager.smallTreesPrefab[0];
                }
            }
            else if (treesHP[i] > 20 && treesHP[i] < 30)
            {
                if (_thisLoadedmanager.middleTreesPrefab.Length > 1)
                {
                    prefab = _thisLoadedmanager.middleTreesPrefab[middle];
                    middle++;
                    if (middle > _thisLoadedmanager.middleTreesPrefab.Length)
                        middle = 0;
                }
                else
                {
                    prefab = _thisLoadedmanager.middleTreesPrefab[0];
                }
            }
            else if (treesHP[i] >= 30)
            {
                if (_thisLoadedmanager.bigTreesPrefab.Length > 1)
                {
                    prefab = _thisLoadedmanager.bigTreesPrefab[big];
                    big++;
                    if (big > _thisLoadedmanager.bigTreesPrefab.Length)
                        big = 0;
                }
                else
                {
                    prefab = _thisLoadedmanager.bigTreesPrefab[0];
                }
            }

            #endregion

            if (prefab != null)
            {
                NewTree = Instantiate(prefab, new Vector3(treesPosition[i][0], treesPosition[i][1], treesPosition[i][2]), new Quaternion(treesRotation[i][0], treesRotation[i][1], treesRotation[i][2], treesRotation[i][3]), _trees.transform);
                if(NewTree != null)
                {
                    NewTree.tag = Tags.StaticObject.ToString();
                    _thisLoadedmanager.staticPropsList.Add(NewTree);
                    NewTree = null;
                }
            }
            ThisPlayerProgressChange(_thisPlayerProgress + (1f / _loadStages) / treesCount);
        }
        DeveloperScreenController.AddInfo("Trees Load...OK", 1);
        SendDataTCP.SendGetSpells(_thisPlayerProgress);
    }

    public static void LoadSpells(short[][][] spells)
    {
        DeveloperScreenController.AddInfo("Begin Load: Spells", 1);
        SpellContainer[] _resourses = Resources.LoadAll("Spells", typeof(SpellContainer)).Cast<SpellContainer>().ToArray();
        DeveloperScreenController.AddInfo("Build Count: " + spells.Length.ToString(), 1);
        DeveloperScreenController.AddInfo("Spells: ", 1);
        for(int i = 0; i < spells.Length; i++)
        {
            GameObject[] spellsObjects = new GameObject[spells[i][0].Length];
            for(int j = 0; j < spells[i][0].Length; j++)
            {
                foreach(SpellContainer container in _resourses)
                {
                    if(container.CheckHash(spells[i][0][j]))
                    {
                        spellsObjects[j] = container.GetSpellVariation(spells[i][1][j]);
                        break;
                    }
                }
            }
            _thisLoadedmanager.spells.Add(i, spellsObjects);
        }
        switch (_race)
        {
            case "Ignis":
                _thisLoadedmanager.Players[ObjectManager.playerIndex].LoadCombinations(_thisLoadedmanager.spells[ObjectManager.playerIndex]);
                break;
        }
        DeveloperScreenController.AddInfo("Spells Load...OK", 1);
        ClientUDP.Create();
        ClientUDP.BeginReceive();
        SendDataUDP.SendConnectionOk();
    }

    public static void LoadComplite()
    {
        SendDataTCP.SendLoadComplite(_thisPlayerProgress);
    }

    #endregion

    #region Private Helpers

    private static void SpawnEnemy(string nickname, float[] pos, float[] rot, int i)
    {
        GameObject _enemy = Instantiate(Resources.Load("Players/Player"), _thisLoadedmanager.GetStartPosition(i), _thisLoadedmanager.GetStartRotation(i)) as GameObject;
        Player _playerComponent = _enemy.GetComponent<Player>();
        _thisLoadedmanager.Players[i] = _playerComponent;
        _playerComponent.Init(i, nickname, _thisLoadedmanager.GetStartPosition(i), _thisLoadedmanager.GetStartRotation(i));
        _enemy.tag = Tags.Enemy.ToString();
        DeveloperScreenController.AddInfo("Enemy " + nickname + " Load...OK", 1);
    }

    private static void SpawnPlayer(string nickname, float[] pos, float[] rot, int i)
    {
        GameObject _player = Instantiate(Resources.Load("Players/Player"), _thisLoadedmanager.GetStartPosition(i), _thisLoadedmanager.GetStartRotation(i)) as GameObject;
        Player _playerComponent = _player.GetComponent<Player>();
        _thisLoadedmanager.Players[i] = _playerComponent;
        _playerComponent.Init(i, nickname, _thisLoadedmanager.GetStartPosition(i), _thisLoadedmanager.GetStartRotation(i), true, true);
        ObjectManager.playerIndex = i;
        ObjectManager.cameraTarger.player = _player.transform;
        _player.tag = Tags.Player.ToString();
        DeveloperScreenController.AddInfo("Player " + nickname + " Load...OK", 1);
    }

    #endregion

    #region LoadScreen

    public static void EnemyProgressChange(float progress)
    {
        DeveloperScreenController.AddInfo("Enemy Load Handled "+progress, 1);
        _enemyPlayerProgress = progress;
    }

    public static void ThisPlayerProgressChange(float progress)
    {
        _thisPlayerProgress = progress;
    }

    #endregion

    public static void StartBattle()
    {
        MainThread.executeInUpdate(() => {
            _loadScreen.SetActive(false);
            BattleLogic.BeginBattle();
        });
 
    }
}
