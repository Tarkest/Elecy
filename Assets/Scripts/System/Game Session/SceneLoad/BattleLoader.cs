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
    private static bool _loaded = false;
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

    private void Update()
    {
        if (_loaded)
        {
            _loaded = false;
            _loadScreen.SetActive(false);
        }
        else
        {
            _yourLoadProgress.value = _thisPlayerProgress;
            _enemyLoadProgress.value = _enemyPlayerProgress;
        }
    }

    public static void LoadScene(int MapIndex)
    {
        MainThread.executeInUpdate(() => Instantiate(Resources.Load("Maps/" + MapIndex + "/GameArea"), Vector3.zero, Quaternion.identity));
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
            ThisPlayerProgressChange(1f / _loadStages + (1f / _loadStages) / 2);
            GameObject _enemy = Instantiate(Resources.Load("Players/Player"), _thisLoadedmanager.GetEnemyStartPosition(), _thisLoadedmanager.GetEnemyStartRotation()) as GameObject;
            _enemy.GetComponent<PlayerStats>().SetStats(1000, 1000, 10f, 10f, 10, 5, 5, 5, 5);
            ObjectManager.players[1] = _enemy;
            DeveloperScreenController.AddInfo("Enemy Load...OK", 1);
            ThisPlayerProgressChange(1f / _loadStages + (1f / _loadStages));
        }
        else
        {
            _thisLoadedmanager.SetStartTransform(positions[1], positions[0], rotations[1], rotations[0]);
            GameObject _player = Instantiate(Resources.Load("Players/Player"), _thisLoadedmanager.GetPlayerStartPosition(), _thisLoadedmanager.GetPlayerStartRotation()) as GameObject;
            _player.GetComponent<PlayerStats>().SetStats(1000, 1000, 10f, 10f, 10, 5, 5, 5, 5);
            ObjectManager.players[1] = _player;
            DeveloperScreenController.AddInfo("Player Load...OK", 1);
            ThisPlayerProgressChange(1f / _loadStages + (1f / _loadStages) / 2);
            GameObject _enemy = Instantiate(Resources.Load("Players/Player"), _thisLoadedmanager.GetEnemyStartPosition(), _thisLoadedmanager.GetEnemyStartRotation()) as GameObject;
            _enemy.GetComponent<PlayerStats>().SetStats(1000, 1000, 10f, 10f, 10, 5, 5, 5, 5);
            ObjectManager.players[0] = _enemy;
            DeveloperScreenController.AddInfo("Enemy Load...OK", 1);
            ThisPlayerProgressChange(1f / _loadStages + (1f / _loadStages));
        }

    }

    private static void 


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
}
