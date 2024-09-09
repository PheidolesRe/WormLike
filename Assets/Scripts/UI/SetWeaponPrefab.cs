using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SetWeaponPrefab : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Weapons weaponName;

    private Button weaponButtonUI;

    private enum Weapons
    { 
        Bazooka,
        SMG,
        Builder,
        FragmentationGrenade,
    }

    private void Awake()
    {
        weaponButtonUI = GetComponent<Button>();

        weaponButtonUI.onClick.AddListener(() =>
        {
            EventBus.OnWeaponChange?.Invoke(weaponName.ToString());
        });
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        EventBus.OnDestroyTransperentBlock?.Invoke();
    }
}
