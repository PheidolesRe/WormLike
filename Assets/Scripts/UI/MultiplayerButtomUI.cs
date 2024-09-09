using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MultiplayerButtomUI : MonoBehaviour
{
    private void Awake()
    {
        Button mpButton = GetComponent<Button>();

        mpButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("CreateGameScene");
        });
    }
}
