using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NetworkUI : MonoBehaviour
{
    [SerializeField] private Button createGameButton;
    [SerializeField] private Button findGameButton;
    [SerializeField] private Button joinCodeButton;
    [SerializeField] private TMP_InputField codeInputField;
    [SerializeField] private TMP_InputField playerNameInputField;
    [SerializeField] private TurnLobbyUI lobbyCreateUI;
    [SerializeField] private Transform lobbyContainer;
    [SerializeField] private Transform lobbyTemplate;
    [SerializeField] private WaitingBackgroundUI waitingBackgroundUI;

    private string waitingJoinGame = "Joining to a Game...";
    private string noCodeString = "Enter the code";

    private void Awake()
    {
        createGameButton.onClick.AddListener(() =>
        {
            lobbyCreateUI.Show();
        });

        findGameButton.onClick.AddListener(() =>
        {
            waitingBackgroundUI.WiatingTillLoad(waitingJoinGame);
            MyLobby.Instance.QuickJoin();
        });

        joinCodeButton.onClick.AddListener(() =>
        {
            if (codeInputField.text != "")
            {
                waitingBackgroundUI.WiatingTillLoad(waitingJoinGame);
                MyLobby.Instance.JoinWithCode(codeInputField.text);
            }
            else
            {
                waitingBackgroundUI.WiatingTillLoad(noCodeString);
            }
        });

        lobbyTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        MyLobby.Instance.OnLobbyListChanged += MyLobby_OnLobbyListChanged;
        UpdateLobbyList(new List<Lobby>());

        playerNameInputField.text = MultiplayerManager.Instance.GetPlayerName();

        playerNameInputField.onValueChanged.AddListener((string newText) =>
        {
            MultiplayerManager.Instance.SetPlayerName(newText); 
        });
    }

    private void MyLobby_OnLobbyListChanged(MyLobby.OnLobbyListChangedEventArgs e)
    {
        UpdateLobbyList(e.lobbyList);
    }

    private void UpdateLobbyList(List<Lobby> lobbyList)
    {
        foreach (Transform child in lobbyContainer)
        {
            if (child == lobbyTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (Lobby lobby in lobbyList)
        {
            Transform lobbyTransform = Instantiate(lobbyTemplate, lobbyContainer);
            lobbyTransform.gameObject.SetActive(true); 
            lobbyTransform.GetComponent<LobbyListSingleUI>().SetLobby(lobby);
        }
    }

}
