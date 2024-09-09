using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootUI : MonoBehaviour
{
    private Button shootButton;


    private void Awake()
    {
        shootButton = GetComponent<Button>();

        shootButton.onClick.AddListener(() =>
        {
            EventBus.OnShoot?.Invoke();
        });
    }
}
