using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ShootWeapon : NetworkBehaviour, IWeaponShoot
{
    [SerializeField] private GameObject ProjectilePrefab;

    [SerializeField] private float shootDelay = 0.2f;
    [SerializeField] private int shootAmountForAttack;

    private void OnEnable()
    {
        EventBus.OnShoot += Attack;
    }

    private void OnDisable()
    {
        EventBus.OnShoot -= Attack;
    }

    public void Attack()
    {
        if (!IsOwner)
        {
            return;
        }

        if (GameStateManager.GAME_STATE.Attack == GameStateManager.Instance.gameState && TurnManager.Instance.IsYourTurn((int)OwnerClientId))
        { 
            EndTurnServerRpc();        
            StartCoroutine(ShootDelayRoutine());            
        }
    }

    private IEnumerator ShootDelayRoutine()
    {
        for (int i = 0; i < shootAmountForAttack; i++)
        { 
            Quaternion weaponAngel = transform.rotation;
            Vector2 newTarget = transform.GetChild(0).position - transform.position;// Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            MakeNewShotServerRpc(transform.GetChild(0).position, weaponAngel, newTarget);

            yield return new WaitForSeconds(shootDelay);        
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void MakeNewShotServerRpc(Vector3 pos, Quaternion rot, Vector2 newTarget)
    {
        GameObject newRocket = Instantiate(ProjectilePrefab, pos, rot);
        newRocket.GetComponent<Projectile>().target = newTarget;
        newRocket.GetComponent<NetworkObject>().Spawn();
    }

    [ServerRpc(RequireOwnership = false)]
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
