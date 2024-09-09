using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    [SerializeField] private TurnManager turnManager; 

    private Image timerImage;

    private void OnEnable()
    {
        EventBus.OnChangeTimerColor += SetTimerColor;
    }

    private void OnDisable()
    {
        EventBus.OnChangeTimerColor -= SetTimerColor;        
    }

    private void Awake()
    {
        timerImage = GetComponent<Image>();
    }

    private void Update()
    {
        timerImage.fillAmount = TurnManager.Instance.GetCurrentTimerInFraction();
    }

    private void SetTimerColor()
    {
        if (turnManager.GetIsRedTurn())
        {
            timerImage.color = Color.red;
        }
        else
        { 
            timerImage.color = Color.blue;
        }
    }
}
