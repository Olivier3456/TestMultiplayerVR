using TMPro;
using Unity.Netcode;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    [SerializeField] private TextMeshProUGUI gameText;


    public override void OnNetworkSpawn()
    {
        Debug.Log($"OnNetworkSpawn: {NetworkManager.Singleton.LocalClientId}.");

        //if (IsServer)
        {
            NetworkManager.Singleton.OnClientConnectedCallback += NetworkManager_OnClientConnectedCallback;
        }
    }


    private void NetworkManager_OnClientConnectedCallback(ulong obj)
    {
        gameText.text = $"{NetworkManager.Singleton.ConnectedClientsList.Count} player(s) in the game.";
    }
}
