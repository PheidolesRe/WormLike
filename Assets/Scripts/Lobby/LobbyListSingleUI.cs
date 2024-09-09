using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;

public class LobbyListSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI lobbyNameText;
    [SerializeField] private WaitingBackgroundUI waitingBackgroundUI;

    private Lobby lobby;
    private string waitingJoinGame = "Joining to a Game...";

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            waitingBackgroundUI.WiatingTillLoad(waitingJoinGame);
            MyLobby.Instance.JoinWithId(lobby.Id);
        });
    }

    public void SetLobby(Lobby lobby)
    { 
        this.lobby = lobby;
        lobbyNameText.text = lobby.Name;
    }
}
