using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class GrenadeProjectile : Projectile
{
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private float explosionDelay;


    protected override void Start()
    {
        base.Start();

        GetComponent<Rigidbody2D>().AddTorque(-7);
        StartCoroutine(GrenadeExplosionRoutine());
    }

    private IEnumerator GrenadeExplosionRoutine()
    {
        yield return new WaitForSeconds(explosionDelay);
        SpawnExplosionServerRpc();
        DestroyProjectileServerRpc();
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

    protected override void FixedUpdate()
    { 
    
    }
}
