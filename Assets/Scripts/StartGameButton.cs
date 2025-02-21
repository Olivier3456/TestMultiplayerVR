using System;
using UnityEngine;

public class StartGameButton : MonoBehaviour
{
    [SerializeField] private LobbyManager lobbyManager;

    [SerializeField] private GameObject startGameButtonGameObject;

    void Start()
    {
        startGameButtonGameObject.SetActive(false);
        lobbyManager.OnGameReadyToStart += LobbyManager_OnGameReadyToStart;

        GameManager.Instance.OnNetworkSpawnEvent += GameManager_OnNetworkSpawn;
    }

    private void GameManager_OnNetworkSpawn(object sender, EventArgs e)
    {
        //Debug.Log("Spawned on network, hiding Start game button.");
        startGameButtonGameObject.SetActive(false);        
    }

    private void LobbyManager_OnGameReadyToStart(object sender, EventArgs e)
    {
        startGameButtonGameObject.SetActive(true);
    }
}
