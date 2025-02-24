using System;
using UnityEngine;
using UnityEngine.UI;

public class StartGameButton : MonoBehaviour
{
    [SerializeField] private GameObject startGameButtonGameObject;

    void Start()
    {
        startGameButtonGameObject.SetActive(false);


        LobbyManager.Instance.OnGameReadyToStart += LobbyManager_OnGameReadyToStart;

        GameManager.Instance.OnNetworkSpawnEvent += GameManager_OnNetworkSpawn;
    }


    private void LobbyManager_OnGameReadyToStart(object sender, EventArgs e)
    {
        if (LobbyManager.Instance.IsLobbyHost())
        {
            startGameButtonGameObject.SetActive(true);
        }
    }

    private void GameManager_OnNetworkSpawn(object sender, EventArgs e)
    {
        //Debug.Log("Spawned on network, hiding Start game button.");
        startGameButtonGameObject.SetActive(false);
    }
}
