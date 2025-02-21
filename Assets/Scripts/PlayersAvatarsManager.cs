using System;
using Unity.Netcode;
using UnityEngine;

public class PlayersAvatarsManager : NetworkBehaviour
{
    public static PlayersAvatarsManager Instance { get; private set; }

    


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Only one instance of PlayerAvatarReceiver is allowed per player!");
            Destroy(this);
            return;
        }
    }


    public override void OnNetworkSpawn()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += NetworkManager_OnClientConnectedCallback;
    }


    private void NetworkManager_OnClientConnectedCallback(ulong obj)
    {

    }


    public void OnPlayerAvatarUpdate(PlayerAvatarData playerAvatarData)
    {

    }
}
