using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SetBuildStateUI : MonoBehaviour, IPointerDownHandler //IDragHandler  IPointerUpHandler
{
    private Button setBuildButton;

    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private StateBlock stateBlock;

    //private GameObject temporarilyBlock;
    //private bool isDown = false;
    //private bool isUp = false;

    private void Awake()
    {
        setBuildButton = GetComponent<Button>();

        setBuildButton.onClick.AddListener(() =>
        {
            switch (stateBlock)
            {
                case StateBlock.Build:
                    GameStateManager.Instance.SetGameStateBuild();
                    break;
                case StateBlock.Attack:
                    GameStateManager.Instance.SetGameStateAttack();
                    break;
            }
            
            inventoryUI.SetActive(false);
        });
    }

    private enum StateBlock
    { 
        Build,
        Attack,
    }

    public void OnPointerDown(PointerEventData eventData)
    {        
        //switch (stateBlock)
        //{ 
        //    case StateBlock.Build:
        //        GameStateManager.Instance.SetGameStateBuild();
        //        break;
        //    case StateBlock.Attack:
        //        GameStateManager.Instance.SetGameStateAttack(); 
        //        break;
        //}

        //inventoryUI.SetActive(false);
    }


}
