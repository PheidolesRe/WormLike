using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CHargableShootUI : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    //private Button chargeAndShootBuuton;

    //private void Awake()
    //{
    //    chargeAndShootBuuton = GetComponent<Button>();
    //}
    public void OnPointerDown(PointerEventData eventData)
    {
        EventBus.OnChargeWeapon?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        EventBus.OnShoot?.Invoke();
    }
}
