using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Player : NetworkBehaviour
{
    //[SerializeField] private GameObject buildManager;
    [SerializeField] private List<Vector2> playerPosList;

    //private bool isRed = false;

    public override void OnNetworkSpawn()
    {
        transform.position = playerPosList[(int)OwnerClientId];

        SetColorAtStartClientRpc();

        //buildManager.GetComponent<BuildManager>().SetOwnerID(OwnerClientId);
        //EventBus.OnPlayerSpawn?.Invoke(OwnerClientId);

        //SpawnBuildManagerServerRpc();
        //if (IsOwner)
        //{ 
        //    buildManager.GetComponent<BuildManager>().ownerId = (int)OwnerClientId;
        //    Instantiate(buildManager);
        //}


        if ((int)OwnerClientId != 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            //isRed = true;
        }
        //transform.rotation = (int)OwnerClientId == 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;  // just showing off
    }

    //[ServerRpc(RequireOwnership = false)]
    //private void SpawnBuildManagerServerRpc(ulong clientId)
    //{        
    //    GameObject clientBuild = Instantiate(buildManager);
    //    clientBuild.GetComponent<NetworkObject>().SpawnWithOwnership(clientId);
    //    Debug.Log((int)clientId);
    //}

    //[ClientRpc]
    //private void SpawnBuildManagerClientRpc()
    //{
    //    if (!IsOwner) return;
    //}

    [ClientRpc]
    private void SetColorAtStartClientRpc()
    {
        if ((int)OwnerClientId == 0)
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(0, 0.5f, 1);
        }
    }
}
