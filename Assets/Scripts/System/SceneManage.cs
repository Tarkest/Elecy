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
        if (scene.buildIndex == NetworkConstants.ENTRANCE_NUMBER)
        {
            Network.state = Network.GameState.Entrance;
            if(Network.isConnected)
            {
                ClientTCP.BeginReceive();
            }
            try
            {
                SceneManager.UnloadSceneAsync(NetworkConstants.MAIN_LOBBY_NUMBER);
            }
            catch { }
        }
        else if (scene.buildIndex == NetworkConstants.MAIN_LOBBY_NUMBER)
        {
            Network.state = Network.GameState.MainLobby;
            NetPlayerTCP.BeginReceive();
            try
            {
                SceneManager.UnloadSceneAsync(NetworkConstants.ENTRANCE_NUMBER);
            }
            catch { }
            try
            {
                SceneManager.UnloadSceneAsync(NetworkConstants.ROOM_ARENA_NUMBER);
            }
            catch { }
        }
        else
        { 
            Network.state = Network.GameState.GameArena;
            RoomTCP.BeginReceive();
        }
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
