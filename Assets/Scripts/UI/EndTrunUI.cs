using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class EndTrunUI : NetworkBehaviour
{
    private Button endTurnButton;

    private void Awake()
    {
        endTurnButton = GetComponent<Button>();

        endTurnButton.onClick.AddListener(() =>
        {
            EndTurnServerRpc();
        });
    }

    [ServerRpc (RequireOwnership = false)]
    private void EndTurnServerRpc()
    {
        EndTurnClientRpc();
    }

    [ClientRpc]
    private void EndTurnClientRpc()
    {
        EventBus.OnTurnEnd?.Invoke();
    }
}
