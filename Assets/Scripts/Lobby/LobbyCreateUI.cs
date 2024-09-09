using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyCreateUI : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    [SerializeField] private Button createPublicButton;
    [SerializeField] private Button createPrivateButton;
    [SerializeField] private TMP_InputField lobbyNameInputField;
    [SerializeField] private WaitingBackgroundUI waitingBackground;

    private string waitinigCreateServer = "Game is creating ...";


    private void Awake()
    {        
        createPublicButton.onClick.AddListener(() =>
        {
            waitingBackground.WiatingTillLoad(waitinigCreateServer);
            MyLobby.Instance.CreateLobby(lobbyNameInputField.text, false);
        });

        createPrivateButton.onClick.AddListener(() =>
        {
            waitingBackground.WiatingTillLoad(waitinigCreateServer);
            MyLobby.Instance.CreateLobby(lobbyNameInputField.text, true);
        });

        closeButton.onClick.AddListener(() =>
        {
            Hide();
        });

    }

    private void Start()
    {
        Hide();
    }

    public void Show()
    { 
        gameObject.SetActive(true);    
    }

    private void Hide()
    { 
        gameObject.SetActive(false);
    }    
}
