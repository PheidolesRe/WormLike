using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildUI : MonoBehaviour
{
    private Button buildButton;

    private void Awake()
    {
        buildButton = GetComponent<Button>();

        buildButton.onClick.AddListener(() =>
        {
            EventBus.OnBuild?.Invoke();
        });
    }
}
