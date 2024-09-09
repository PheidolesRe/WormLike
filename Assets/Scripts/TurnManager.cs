using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : Singletone<TurnManager>
{
    [SerializeField] private float maxMoveTimer;
    [SerializeField] private float switchTimer;

    private float currentTimer;

    private bool isRedTurn;
    private bool isBlueTurn;
    private bool isSwitching = false;

    protected override void Awake()
    {
        base.Awake();

        SetRedTurn();
    }

    private void OnEnable()
    {
        EventBus.OnTurnEnd += SwitchTurn;
    }

    private void OnDisable()
    {
        EventBus.OnTurnEnd -= SwitchTurn;
    }

    private void Update()
    {
        RunTimer();
    }

    public void SetRedTurn()
    { 
        isRedTurn = true;
        isBlueTurn = false;

        SetMaxTimer();
    }

    public void SetBlueTurn()
    {
        isRedTurn = false;
        isBlueTurn = true;

        SetMaxTimer();
    }

    public bool GetIsRedTurn()
    { 
        return isRedTurn;
    }

    public void SwitchTurn()
    { 
        StartCoroutine(SwitchTurnRoutine());
    }

    private IEnumerator SwitchTurnRoutine()
    {
        isSwitching = true;
        isRedTurn = !isRedTurn;
        isBlueTurn = !isBlueTurn;
        SetMaxTimer();
        EventBus.OnUseProhibitedSwitch?.Invoke();
        yield return new WaitForSeconds(switchTimer);

        isSwitching = false;
        EventBus.OnUseProhibitedSwitch?.Invoke();

    }

    public bool IsYourTurn(int OwnerId)
    {
        if (isSwitching) return false;

        if (OwnerId == 0)
        { 
            return isRedTurn;        
        }
        else
        {
            return isBlueTurn;
        }

    }

    private void RunTimer()
    {
        if (!isSwitching)
        { 
            currentTimer -= Time.deltaTime;
        }

        if (currentTimer < 0) 
        {
            SwitchTurn();
        }
    }

    private void SetMaxTimer()
    {
        EventBus.OnChangeTimerColor?.Invoke();
        currentTimer = maxMoveTimer;
    }

    public float GetCurrentTimerInFraction()
    {
        return currentTimer / maxMoveTimer;
    }
}
