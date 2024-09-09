using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class ChargableWeapon : NetworkBehaviour
{
    [SerializeField] private float chargeSpeed;
    [SerializeField] private float maxCharge;

    private float powerOfCharge = 0f;
    private bool isCharging = false;

    [SerializeField] private GameObject ProjectilePrefab;

    private void OnEnable()
    {
        EventBus.OnShoot += Attack;
        EventBus.OnChargeWeapon += SetOnCharge;
    }

    private void OnDisable()
    {
        EventBus.OnShoot -= Attack;
        EventBus.OnChargeWeapon -= SetOnCharge;
    }

    private void Update()
    {
        Charging();
    }

    private void Attack()
    {
        if (!IsOwner)
        {
            return;
        }

        if (GameStateManager.GAME_STATE.Attack == GameStateManager.Instance.gameState && TurnManager.Instance.IsYourTurn((int)OwnerClientId))
        {
            Quaternion weaponAngel = transform.rotation;
            Vector2 newTarget = transform.GetChild(1).position - transform.position;// Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            MakeNewShotServerRpc(transform.GetChild(1).position, weaponAngel, newTarget, powerOfCharge / maxCharge);
            Debug.Log(powerOfCharge);
            ResetCharge();
            EndTurnServerRpc();
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void MakeNewShotServerRpc(Vector3 pos, Quaternion rot, Vector2 newTarget, float power)
    {
        GameObject newRocket = Instantiate(ProjectilePrefab, pos, rot);
        newRocket.GetComponent<Projectile>().target = newTarget;
        newRocket.GetComponent<Projectile>().SetPowerCoef(power);
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
    private void SetOnCharge()
    {
        if (TurnManager.Instance.IsYourTurn((int)OwnerClientId))
        { 
            isCharging = true;        
        }
    }

    private void Charging()
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().size = new Vector2 (0.05f, powerOfCharge/ maxCharge * 0.3f);
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.Lerp(Color.green, Color.red, powerOfCharge / maxCharge);

        if (!TurnManager.Instance.IsYourTurn((int)OwnerClientId) && powerOfCharge > 0)
        {
            Quaternion weaponAngel = transform.rotation;
            Vector2 newTarget = transform.GetChild(1).position - transform.position;// Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            MakeNewShotServerRpc(transform.GetChild(1).position, weaponAngel, newTarget, powerOfCharge / maxCharge);
            Debug.Log(powerOfCharge);
            ResetCharge();
        }

        if (!TurnManager.Instance.IsYourTurn((int)OwnerClientId)) ResetCharge();

        if (!isCharging || !IsOwner || !TurnManager.Instance.IsYourTurn((int)OwnerClientId)) return;
                
        if (powerOfCharge < maxCharge)
        {
            powerOfCharge += Time.deltaTime * chargeSpeed;
        }
        else 
        {
            powerOfCharge = maxCharge;
        }
    }

    private void ResetCharge()
    {
        isCharging = false;
        powerOfCharge = 0f;
    }

    
}
