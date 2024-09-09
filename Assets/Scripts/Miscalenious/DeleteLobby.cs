using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteLobby : MonoBehaviour
{
    private void Start()
    {
        if (MyLobby.Instance != null)
        { 
            Destroy(MyLobby.Instance);
        }
    }
}
