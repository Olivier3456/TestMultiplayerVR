using System;
using TMPro;
using UnityEngine;

public class SessionUI : MonoBehaviour
{
    [SerializeField] private GameObject startSessionButtonGameObject;

    [SerializeField] private TextMeshProUGUI sessionText;

    void Start()
    {
        startSessionButtonGameObject.SetActive(false);

        LobbyManager.Instance.OnGameReadyToStart += LobbyManager_OnGameReadyToStart;

        SessionManager.Instance.OnSessionJoin += SessionManager_OnSessionJoin;
    }


    private void LobbyManager_OnGameReadyToStart(object sender, EventArgs e)
    {
        if (LobbyManager.Instance.IsLobbyHost())
        {
            startSessionButtonGameObject.SetActive(true);
        }
    }

    private void SessionManager_OnSessionJoin(object sender, EventArgs e)
    {
        startSessionButtonGameObject.SetActive(false);
        sessionText.text = "Session joined!";
    }
}
