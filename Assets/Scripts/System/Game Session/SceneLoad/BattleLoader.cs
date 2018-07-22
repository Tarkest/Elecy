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

    }

    public static void SceneLoaded(ObjectManager LoadedManager)
    {
        _thisLoadedmanager = LoadedManager;
        _loadStages += 3;
        if (_thisLoadedmanager.bigRocksPrefab.Length > 0|| _thisLoadedmanager.middleRocksPrefab.Length > 0|| _thisLoadedmanager.smallRocksPrefab.Length > 0)
            _loadStages += 1;
        if (_thisLoadedmanager.bigTreesPrefab.Length > 0 || _thisLoadedmanager.middleTreesPrefab.Length > 0 || _thisLoadedmanager.smallTreesPrefab.Length > 0)
            _loadStages += 1;
        ThisPlayerProgressChange(1f / _loadStages);
    }

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
