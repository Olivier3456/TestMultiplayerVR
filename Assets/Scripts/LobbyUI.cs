using System;
using TMPro;
using UnityEngine;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI lobbyText;
    [SerializeField] private GameObject createLobbyButtonGameObject;
    [SerializeField] private GameObject joinLobbyButtonGameObject;


    void Start()
    {
        LobbyManager.Instance.OnLobbyJoined += LobbyManager_OnLobbyJoined;
        LobbyManager.Instance.OnKickedFromLobby += LobbyManager_OnKickedFromLobby;
        LobbyManager.Instance.OnLobbyPlayersNumberChange += LobbyManager_OnLobbyPlayersNumberChange;

        SessionManager.Instance.OnSessionJoin += SessionManager_OnNetworkSpawnEvent;
    }


    private void LobbyManager_OnLobbyJoined(object sender, LobbyManager.LobbyEventArgs e)
    {
        createLobbyButtonGameObject.SetActive(false);
        joinLobbyButtonGameObject.SetActive(false);

        lobbyText.text = $"Joigned lobby. Players in lobby: {e.lobby.Players.Count}";
    }

    private void LobbyManager_OnKickedFromLobby(object sender, LobbyManager.LobbyEventArgs e)
    {
        createLobbyButtonGameObject.SetActive(true);
        joinLobbyButtonGameObject.SetActive(true);

        lobbyText.text = $"Kicked from lobby {e.lobby.Name}";
    }

    private void LobbyManager_OnLobbyPlayersNumberChange(object sender, LobbyManager.LobbyEventArgs e)
    {
        lobbyText.text = $"Joigned lobby. Players in lobby: {e.lobby.Players.Count}";
    }

    private void SessionManager_OnNetworkSpawnEvent(object sender, EventArgs e)
    {
        createLobbyButtonGameObject.SetActive(false);
        joinLobbyButtonGameObject.SetActive(false);
    }
}
