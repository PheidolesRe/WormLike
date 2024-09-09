using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CleanUpSingletone : MonoBehaviour
{
    private void Start()
    {
        if (MultiplayerManager.Instance != null)
        { 
            Destroy(MultiplayerManager.Instance.gameObject);
        }

        if (MyLobby.Instance != null)
        { 
            Destroy(MyLobby.Instance.gameObject);
        }

        if (NetworkManager.Singleton != null)
        {
            Destroy(NetworkManager.Singleton.gameObject);
        }
    }
}
