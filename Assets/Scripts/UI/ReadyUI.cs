using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReadyUI : MonoBehaviour
{
    [SerializeField] private Button readyButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private CharacterSelectReady characterSelectReady;
    [SerializeField] private TextMeshProUGUI lobbyNameText;
    [SerializeField] private TextMeshProUGUI lobbyCodeText;

    private void Awake()
    {
        readyButton.onClick.AddListener(() =>
        {
            characterSelectReady.SetPlayerReady();
        });

        mainMenuButton.onClick.AddListener(() =>
        {
            MyLobby.Instance.LeaveLobby();
            SceneManager.LoadScene("MainMenu");
        });

    }

    private void Start()
    {
        lobbyNameText.text = "Lobby Name: " + MyLobby.Instance.GetLobby().Name;
        lobbyCodeText.text = "Lobby Code: " + MyLobby.Instance.GetLobby().LobbyCode;
    }
}
