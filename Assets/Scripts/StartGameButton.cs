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
    }

    private void LobbyManager_OnGameReadyToStart(object sender, EventArgs e)
    {
        startGameButtonGameObject.SetActive(true);
    }
}
