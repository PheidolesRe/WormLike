using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MultiplayerManager : NetworkBehaviour
{

    private const int MAX_PLAYER_AMPOUNT = 2;

    private NetworkList<PlayerData> playerDateNetworkList;

    private const string PLAYER_PREFS_PLAYER_NAME_MULTIPLAYER = "PlayerNameMultiplayer";

    private string playerName;

    public static MultiplayerManager Instance { get; private set; }
    public event EventHandler OnPlayerDataNetworkListChanged;

    private void Awake()
    {
        Instance = this;

        DontDestroyOnLoad(gameObject);

        playerName = PlayerPrefs.GetString(PLAYER_PREFS_PLAYER_NAME_MULTIPLAYER, "PlayerName " + UnityEngine.Random.Range(1, 1000));
        playerDateNetworkList = new NetworkList<PlayerData>();
        playerDateNetworkList.OnListChanged += PlayerDateNetworkList_OnListChanged;
    }

    public string GetPlayerName()
    {
        return playerName;
    }

    public void SetPlayerName(string playerName)
    { 
        this.playerName = playerName;

        PlayerPrefs.SetString(PLAYER_PREFS_PLAYER_NAME_MULTIPLAYER ,playerName);
    }

    private void PlayerDateNetworkList_OnListChanged(NetworkListEvent<PlayerData> changeEvent)
    {
        OnPlayerDataNetworkListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void StartHost()
    {
        NetworkManager.Singleton.ConnectionApprovalCallback += NetworkManager_ConnectionApprovalCallback;
        NetworkManager.Singleton.OnClientConnectedCallback += NetworkManager_OnClientConnectedCallback;
        NetworkManager.Singleton.StartHost();
        NetworkManager.Singleton.SceneManager.LoadScene("LobbyScene", LoadSceneMode.Single);
    }

    private void NetworkManager_OnClientConnectedCallback(ulong clientId)
    {
        playerDateNetworkList.Add(new PlayerData
        {
            clientId = clientId,
            playerName = this.playerName,
        });

        SetPlayerName(GetPlayerName());
    }

    private void NetworkManager_ConnectionApprovalCallback(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response)
    {

        if (SceneManager.GetActiveScene().name != "LobbyScene")
        { 
            response.Approved = false;
            response.Reason = "Game has already started";
            return;
        }

        if (NetworkManager.Singleton.ConnectedClientsIds.Count >= MAX_PLAYER_AMPOUNT)
        { 
            response.Approved = false;
            response.Reason = "Game is full";
            return;            
        }

        response.Approved = true;
    }

    public void StartClient()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += NetworkManager_Clent_OnClientConnectedCallback;

        NetworkManager.Singleton.StartClient();
    }

    private void NetworkManager_Clent_OnClientConnectedCallback(ulong clientId)
    {
        SetPlayerNameServerRpc(GetPlayerName());
    }

    [ServerRpc (RequireOwnership = false)]
    private void SetPlayerNameServerRpc(string plaerName, ServerRpcParams serverRpcParams = default)
    {
        int playerDateIndex = GetPlayerDataIndexFromClientId(serverRpcParams.Receive.SenderClientId);

        PlayerData playerData = playerDateNetworkList[playerDateIndex];

        playerData.playerName = plaerName;

        playerDateNetworkList[playerDateIndex] = playerData;
    }

    public bool IsPlayerIndexConnected(int playerIndex)
    { 
        return playerIndex < playerDateNetworkList.Count;
    }

    public PlayerData GetPlayerDataFromPlayerIndex(int playerIndex)
    { 
        return playerDateNetworkList[playerIndex];
    }

    public int GetPlayerDataIndexFromClientId(ulong clientId)
    {
        for (int i = 0; i < playerDateNetworkList.Count; i ++)
        {
            if (playerDateNetworkList[i].clientId == clientId)
            { 
                return i;
            }
        }

        return -1;
    }

}
