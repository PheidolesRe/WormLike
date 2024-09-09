using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public static class EventBus 
{
    public static Action OnTurnEnd;
    public static Action<ulong> OnPlayerSpawn;

    // Weapons
    public static Action<string> OnWeaponChange;
    public static Action OnShoot;
    public static Action OnBuild;
    public static Action OnSetBuilding;
    public static Action OnChargeWeapon;
    public static Action OnBloockRotated;
    public static Action OnDestroyTransperentBlock;
    public static Action<bool> OnTransperentBlockOverlap;

    //UI
    public static Action OnChangeTimerColor;

    public static Action<bool> OnGameOver;

    public static Action OnUseProhibitedSwitch;

}
