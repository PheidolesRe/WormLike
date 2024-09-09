using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleEllemntUI : MonoBehaviour
{
    [SerializeField] private GameObject BuildUI;
    [SerializeField] private GameObject AttackUI;
    [SerializeField] private GameObject ChargableAttackUI;
    [SerializeField] private ELLEMENT_TYPE type;

    private enum ELLEMENT_TYPE
    {
        Build, 
        Attack, 
        ChargableAttack
    }

    private Dictionary<string, GameObject> ellementUIDictionary; 
    
    private void Awake()
    {
        ellementUIDictionary = new Dictionary<string, GameObject>
        {
            {"Build", BuildUI},
            {"Attack", AttackUI},
            {"ChargableAttack", ChargableAttackUI}
        };

        gameObject.GetComponent<Button>().onClick.AddListener(() =>
        {
            foreach (var ellement in ellementUIDictionary)
            {
                if (ellement.Key == type.ToString())
                {
                    ellement.Value.gameObject.SetActive(true);
                }
                else
                { 
                    ellement.Value.gameObject.SetActive(false);
                }
            }
        });
    }
}
