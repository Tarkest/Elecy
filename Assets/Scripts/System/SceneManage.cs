using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == NetworkConstants.ENTRANCE_NUMBER)
        {
            if(Network.Connect == Network.ConnectStatus.Unconnected)
            {
                ClientTCP.Init();
            }
            try
            {
                SceneManager.UnloadSceneAsync(NetworkConstants.MAIN_LOBBY_NUMBER);
            }
            catch { }
        }
        else if (scene.buildIndex == NetworkConstants.MAIN_LOBBY_NUMBER)
        {
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
    }

}
