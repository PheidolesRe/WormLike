using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class SetWeapon : NetworkBehaviour
{
    [SerializeField] private GameObject bazookaWeapon;
    [SerializeField] private GameObject builderWeapon;
    [SerializeField] private GameObject sMGWeapon;
    [SerializeField] private GameObject fragGrenade;

    private Dictionary<string, GameObject> weaponDic;

    private GameObject currentWeapon;

    private void OnEnable()
    {
        EventBus.OnWeaponChange += SetNewWeapon;
    }

    private void Awake()
    {
        weaponDic = new Dictionary<string, GameObject>
        {
            {"Bazooka",  bazookaWeapon},
            {"Builder",  builderWeapon},
            {"SMG",  sMGWeapon},
            {"FragmentationGrenade",  fragGrenade},
        };
    }

    private void OnDisable()
    {
        EventBus.OnWeaponChange -= SetNewWeapon;
    }

    private void SetNewWeapon(string weaponName)
    {
        if (!IsOwner)
        { 
            return;
        }

        WeaponSpawnServerRpc(weaponName);      
    }

    [ServerRpc(RequireOwnership = false)]
    private void WeaponSpawnServerRpc(string weaponName)
    {
        if (currentWeapon != null)
        { 
            currentWeapon.GetComponent<NetworkObject>().Despawn();
        }

        currentWeapon = Instantiate(weaponDic[weaponName], transform.position, Quaternion.identity);
        currentWeapon.GetComponent<NetworkObject>().SpawnWithOwnership(OwnerClientId);

        currentWeapon.transform.parent = transform;      
        //currentWeapon.GetComponent<BuildManager>().SpawnTransperentBlock();
    }

    

}
