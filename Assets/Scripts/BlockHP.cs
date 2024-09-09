using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class BlockHP : NetworkBehaviour
{
    [SerializeField] private float maxHP;

    private float currentHP;

    private void Awake()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        CheckDeath();
    }

    private void CheckDeath()
    {
        if (currentHP <= 0)
        {
            DestroyBlockServerRpc();
        }
    }

    public void DestroySelf()
    {
        DestroyBlockServerRpc();
    }

    [ServerRpc(RequireOwnership = false)]
    private void DestroyBlockServerRpc()
    {
        gameObject.GetComponent<NetworkObject>().Despawn(gameObject);
    }
}