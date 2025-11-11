using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance;

    [SerializeField] private Image gameOverImage;
    [SerializeField] private float restartDelay = 2f;

    private bool isGameOver = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if(gameOverImage != null)
        {
            gameOverImage.gameObject.SetActive(false);
        }
    }

    public void TriggerGameOver()
    {
        if (isGameOver) return;
        isGameOver = true;
        Debug.Log("Game Over triggered.");

        if (gameOverImage != null)
        {
            gameOverImage.gameObject.SetActive(true);
        }

        Time.timeScale = 0f; // Pause the game

        StartCoroutine(RestartLevelAfterDelay());
    }

    private IEnumerator RestartLevelAfterDelay()
    {
        yield return new WaitForSecondsRealtime(restartDelay);
        Time.timeScale = 1f; // Resume the game

        if (GameController.Instance != null)
        {
            int currentLevel = GameController.Instance.currentLevelIndex;
            Debug.Log("Restarting Level: " + currentLevel);
            GameController.Instance.RestartCurrentLevel();
        }
        else
        {
            Debug.LogWarning("GameController instance not found. Cannot restart level.");
        }
        isGameOver = false;
    }

    public void HideGameOver()
    {
        if(gameOverImage != null)
        {
            gameOverImage.gameObject.SetActive(false);
        }
        Time.timeScale = 1f; // Resume the game
    }


}
