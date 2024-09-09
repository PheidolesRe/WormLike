using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnLobbyUI : MonoBehaviour
{
    [SerializeField] private GameObject lobbyCreateUI;

    public void Show()
    { 
        lobbyCreateUI.SetActive(true);
    }

    public void Hide() 
    {
        lobbyCreateUI.SetActive(false);
    }
}
