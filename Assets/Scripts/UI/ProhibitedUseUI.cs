using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProhibitedUseUI : MonoBehaviour
{
    [SerializeField] private GameObject prohibitedArea;

    private void OnEnable()
    {
        EventBus.OnUseProhibitedSwitch += SwithProhibited;
    }

    private void OnDisable()
    {
        EventBus.OnUseProhibitedSwitch -= SwithProhibited;        
    }

    private void SwithProhibited()
    { 
        prohibitedArea.SetActive(!prohibitedArea.activeSelf);
    }
}
