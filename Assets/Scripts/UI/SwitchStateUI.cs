using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchStateUI : MonoBehaviour
{
    [SerializeField] private Button attackButton;
    [SerializeField] private Button buildButton;
    //[SerializeField] private GameStateManager gameStateManager;

    private void Awake()
    {
        attackButton.onClick.AddListener(() =>
            {
                GameStateManager.Instance.SetGameStateAttack();
            }); 
        
        buildButton.onClick.AddListener(() =>
            {
                GameStateManager.Instance.SetGameStateBuild();
            }); 
    }
}
