using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideUIEllement : MonoBehaviour
{
    [SerializeField] GameObject ellementUI;

    private Button toggleButton;

    private void Awake()
    {
        toggleButton = GetComponent<Button>();
        toggleButton.onClick.AddListener(() =>
        {
            ellementUI.SetActive(!ellementUI.activeSelf);

            if (ellementUI.activeSelf)
            {
                GameStateManager.Instance.SetGameStateChoosing();
            }
        });
    }

}
