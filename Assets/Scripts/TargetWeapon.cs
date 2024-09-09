using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class TargetWeapon : NetworkBehaviour
{
    //[SerializeField] private GameObject ProjectilePrefab;

    private Quaternion lastRotation;
    private float floorPosition = -4.6f;

    public override void OnNetworkSpawn()
    {
        if ((int)OwnerClientId != 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    void Update()
    {
        if (!IsOwner)
        {
            return;
        }

        RotateTransformServerRpc(GetWeaponAdjasment()); // It works, but why only with ServerRpc ?!
    }


    private Quaternion GetWeaponAdjasment()
    {
        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).y < floorPosition)
        { 
            return lastRotation;
        }

        Vector2 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float weaponAngel = Mathf.Atan(MousePos.y / MousePos.x) * Mathf.Rad2Deg;

        if (MousePos.x <= 0)
        {
            lastRotation = Quaternion.Euler(0, 0, weaponAngel + 90);
            return lastRotation;
        }
        else 
        {
            lastRotation = Quaternion.Euler(0, 0, weaponAngel - 90); 
            return lastRotation;
        }
    }

    

    [ServerRpc (RequireOwnership = false)]
    private void RotateTransformServerRpc(Quaternion rotation)
    {
        transform.rotation = rotation;      
    }

}
