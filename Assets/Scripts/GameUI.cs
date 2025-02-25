using System;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gameText;

    void Start()
    {
        gameText.text = "";
        GameManager.Instance.OnGameStart += SessionManager_OnGameStart;

        SessionManager.Instance.OnSessionJoin += SessionManager_OnSessionJoin;
    }

    private void SessionManager_OnSessionJoin(object sender, EventArgs e)
    {
        gameText.text = "Waiting for another player to start the game...";
    }

    private void SessionManager_OnGameStart(object sender, EventArgs e)
    {
        gameText.text = "Game started!";
        //gameText.text = playersCount < 2 ? "Session joigned. Waiting for the other player to start the game." : "Game started!";
    }
}
