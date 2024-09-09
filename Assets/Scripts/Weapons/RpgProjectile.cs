using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class RpgProjectile : Projectile
{
    [SerializeField] private GameObject explosionPrefab;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Solid solid = collision.gameObject.GetComponent<Solid>();

        if (solid)
        {
            SpawnExplosionServerRpc();
            DestroyProjectileServerRpc();
        }
    }

    [ServerRpc]
    private void SpawnExplosionServerRpc()
    {
        SpawnExplosionClientRpc();
    }

    [ClientRpc]
    private void SpawnExplosionClientRpc()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
    }

    [ServerRpc]
    private void DestroyProjectileServerRpc()
    {
        DestroyProjectileClientRpc();
    }

    [ClientRpc]
    private void DestroyProjectileClientRpc()
    {
        Destroy(gameObject);
    }
}
