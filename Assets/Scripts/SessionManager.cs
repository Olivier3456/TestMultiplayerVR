using System;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class SessionManager : NetworkBehaviour
{
    public static SessionManager Instance { get; private set; }

    public bool IsSpawn { get; private set; }


    public event EventHandler OnSessionJoin;
    public event EventHandler OnSessionQuit;
    public event EventHandler OnClientConnected;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Only one instance of SessionManager is allowed!");
            Destroy(this);
        }
    }


    public override void OnNetworkSpawn()
    {
        IsSpawn = true;

        //if (IsServer)
        {
            NetworkManager.Singleton.OnClientConnectedCallback += NetworkManager_OnClientConnectedCallback;
        }

        Debug.Log("SessionManager - OnNetworkSpawn");
        OnSessionJoin?.Invoke(this, EventArgs.Empty);
    }


    public override void OnNetworkDespawn()
    {
        IsSpawn = false;
        OnSessionQuit?.Invoke(this, EventArgs.Empty);
    }


    private void NetworkManager_OnClientConnectedCallback(ulong obj)
    {
        OnClientConnected?.Invoke(this, EventArgs.Empty);
    }
}
