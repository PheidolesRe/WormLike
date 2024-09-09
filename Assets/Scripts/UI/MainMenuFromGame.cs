using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuFromGame : MonoBehaviour
{
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;
    [SerializeField] private GameObject areYouSureObject;

    private const string MAIN_MENU_SCENE_NAME = "MainMenu";

    private void Start()
    {
        Hide();
    }

    private void Awake()
    {
        mainMenuButton.onClick.AddListener(() =>
        {
            Show();
        });

        yesButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.Shutdown();
            SceneManager.LoadScene(MAIN_MENU_SCENE_NAME);
        });

        noButton.onClick.AddListener(() =>
        {
            Hide();
        });
    }

    private void Show()
    {
        areYouSureObject.SetActive(true);
    }

    private void Hide()
    { 
        areYouSureObject.SetActive(false);        
    }

}
