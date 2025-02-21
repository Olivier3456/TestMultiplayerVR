using TMPro;
using Unity.Netcode;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    [SerializeField] private TextMeshProUGUI gameText;

    public static GameManager Instance { get; private set; }

    public bool IsInGame { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Only one instance of GameManager is allowed!");
            Destroy(this);
        }
    }

    public override void OnNetworkSpawn()
    {
        Debug.Log($"[GameManager] OnNetworkSpawn: {NetworkManager.Singleton.LocalClientId}.");
        IsInGame = true;

        //if (IsServer)
        {
            NetworkManager.Singleton.OnClientConnectedCallback += NetworkManager_OnClientConnectedCallback;
        }
    }

    public override void OnNetworkDespawn()
    {
        IsInGame = false;
        Debug.Log($"[GameManager] OnNetworkDespawn.");
    }


    private void NetworkManager_OnClientConnectedCallback(ulong obj)
    {
        gameText.text = $"Game started. {NetworkManager.Singleton.ConnectedClientsList.Count} player(s) in the game.";
    }
}
