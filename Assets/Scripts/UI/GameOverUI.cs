using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject gameOverText;
    [SerializeField] private Button mainMenuButton;

    private const string MAIN_MENU_SCENE_NAME = "MainMenu";

    private void Awake()
    {
        mainMenuButton.onClick.AddListener(() =>
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(MAIN_MENU_SCENE_NAME);
        });
    }

    private void OnEnable()
    {
        EventBus.OnGameOver += ShowWinner;

    }

    private void OnDisable()
    {
        EventBus.OnGameOver -= ShowWinner;        
    }

    private void ShowWinner(bool isRed)
    {
        Debug.Log("Show");
        transform.GetChild(0).gameObject.SetActive(true);

        if (!isRed)
        {
            gameOverText.GetComponent<TextMeshProUGUI>().text = "RED WON";
            gameOverText.GetComponent<TextMeshProUGUI>().color = Color.red;
        }
        else
        {
            gameOverText.GetComponent<TextMeshProUGUI>().text = "BLUE WON";            
            gameOverText.GetComponent<TextMeshProUGUI>().color = Color.blue;
        }
    }
}
