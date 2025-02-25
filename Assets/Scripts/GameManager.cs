using System;
using Unity.Netcode;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField, Range(1, 4)] private int minPlayerCountToStartGame = 1;

    public static GameManager Instance { get; private set; }

    public event EventHandler OnGameStart;


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


    void Start()
    {
        SessionManager.Instance.OnClientConnected += SessionManager_OnClientConnected;
    }

    private void SessionManager_OnClientConnected(object sender, EventArgs e)
    {
        int playersCount = NetworkManager.Singleton.ConnectedClientsList.Count;

        if (playersCount >= minPlayerCountToStartGame)
        {
            OnGameStart?.Invoke(this, EventArgs.Empty);
        }
    }
}
