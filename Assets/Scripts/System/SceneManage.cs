using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour {

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    //Addenterance
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == NetworkConstants.MAIN_LOBBY_NUMBER)
            NetPlayerTCP.BeginReceive();
        if (scene.buildIndex == NetworkConstants.ROOM_ARENA_NUMBER)
            RoomTCP.BeginReceive();
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
