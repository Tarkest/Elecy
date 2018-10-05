using UnityEngine;

public class PlayerLoader : BaseLoader
{

    public override void Load()
    {
        SendDataTCP.SendBeginLoading(0f);
    }

    public void SpawnPlayers(Vector3[] positions, Quaternion[] rotations)
    {
        Network.currentManager.startPositions = positions;
        Network.currentManager.startRotations = rotations;
        for (int i = 0; i < Network.playerCount; i++)
        {
            if (i == Network.playerIndex)
            {
                SpawnPlayer(positions[i], rotations[i], i);
            }
            else
                SpawnEnemy(positions[i], rotations[i], i);
        }
        BattleLoader.Next();
    }

    private void SpawnEnemy(Vector3 pos, Quaternion rot, int i)
    {
        GameObject _enemy = Instantiate(Resources.Load("Players/Player"), pos, rot) as GameObject;
        Player _playerComponent = _enemy.GetComponent<Player>();
        Network.currentManager.Players[i] = _playerComponent;
        _playerComponent.Init(i, Network.nicknames[i], pos, rot);
        _enemy.tag = Tags.Enemy.ToString();
    }

    private void SpawnPlayer(Vector3 pos, Quaternion rot, int i)
    {
        GameObject _player = Instantiate(Resources.Load("Players/Player"), pos, rot) as GameObject;
        Player _playerComponent = _player.GetComponent<Player>();
        Network.currentManager.Players[i] = _playerComponent;
        _playerComponent.Init(i, Network.nicknames[i], pos, rot, true, true);
        ObjectManager.playerIndex = i;
        ObjectManager.cameraTarger.player = _player.transform;
        _player.tag = Tags.Player.ToString();
    }
}

