using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WhoTurn : MonoBehaviour
{
    [SerializeField] private Transform whoTurnUI;

    private void OnEnable()
    {
        EventBus.OnChangeTimerColor += Show;
    }

    private void OnDisable()
    {
        EventBus.OnChangeTimerColor -= Show;
    }

    private void Awake()
    {
        whoTurnUI.gameObject.SetActive(false);
    }

    private void Show()
    {
        if (TurnManager.Instance.GetIsRedTurn())
        {
            whoTurnUI.GetComponent<TextMeshProUGUI>().text = "Red Turn";
            whoTurnUI.GetComponent<TextMeshProUGUI>().color = Color.red;
        }
        else
        { 
            whoTurnUI.GetComponent<TextMeshProUGUI>().text = "Blue Turn";            
            whoTurnUI.GetComponent<TextMeshProUGUI>().color = Color.blue;
        }

        StartCoroutine(ShowRoutine());
    }


    private IEnumerator ShowRoutine()
    { 
        whoTurnUI.gameObject.SetActive(true);

        yield return new WaitForSeconds(1);

        whoTurnUI.gameObject.SetActive(false);
    }

}
