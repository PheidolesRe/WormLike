using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class SmgProjectile : Projectile
{
    [SerializeField] private float smgBulletDamage = 16;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Solid solid = collision.gameObject.GetComponent<Solid>();
        BlockHP blockHP = collision.gameObject.GetComponent<BlockHP>();

        if (solid)
        {
            if (blockHP) 
            {
                blockHP.TakeDamage(smgBulletDamage);
            }

            DestroyProjectileServerRpc();
        }        
    }

    [ServerRpc]
    private void DestroyProjectileServerRpc()
    {
        gameObject.GetComponent<NetworkObject>().Despawn();
    }
}
