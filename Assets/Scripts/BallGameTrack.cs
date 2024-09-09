using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGameTrack : MonoBehaviour
{
    [SerializeField] private bool isRed;
    [SerializeField] private GameOverUI gameOverUI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameOver gameOverArea = collision.gameObject.GetComponent<GameOver>();

        if (gameOverArea)
        { 
            EventBus.OnGameOver?.Invoke(isRed);
            Time.timeScale = 0f;
        }
    }
}
