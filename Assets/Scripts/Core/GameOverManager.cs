using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance;

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button menuButton;
    [SerializeField] private float restartDelay = 0.5f;

    private float faceDuration = 1f;

    public bool isMenuOpen = false;
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

        if(gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
        if (retryButton != null)
        {
            retryButton.onClick.AddListener(OnRetryButtonClicked);
        }
        if(menuButton != null)
        {
            menuButton.onClick.AddListener(ReturnToMainMenu);
        }
    }

    /*
    private void Start()
    {
        if(gameOverPanel != null)
        {
            gameOverPanel.gameObject.SetActive(false);
        }
    }*/



    public void TriggerGameOver()
    {
        if (isGameOver) return;
        isGameOver = true;
        Debug.Log("Game Over triggered.");

        if (GameController.Instance != null)
        {
            int currentLevel = GameController.Instance.currentLevelIndex;
            if(currentLevel >=0 && currentLevel < GameController.Instance.levels.Length)
            {
                GameController.Instance.levels[currentLevel].StopAllCoroutines();
            }
                
        }

        StartCoroutine(ShowGameOverAfterDelay());

        /*
        if (gameOverPanel != null)
        {

            gameOverPanel.gameObject.SetActive(true);
        }*/

        Time.timeScale = 0f; // Pause the game

        //StartCoroutine(RestartLevelAfterDelay());

    }

    private IEnumerator ShowGameOverAfterDelay()
    {

       yield return new WaitForSecondsRealtime(restartDelay);
        if (gameOverPanel != null)
        {
            gameOverPanel.gameObject.SetActive(true);
        }
        Time.timeScale = 0f; // Pause the game
    }


    private void OnRetryButtonClicked()
    {
        Debug.Log("Retry button clicked.");
        if(GameController.Instance != null)
        {
            int currentLevel = GameController.Instance.currentLevelIndex;
            gameOverPanel.gameObject.SetActive(false);
            Time.timeScale = 1f; // Resume the game
            isGameOver = false;
            GameController.Instance.RestartCurrentLevel();
        }
        else
        {
                        Debug.LogWarning("GameController instance not found. Cannot restart level.");
        
        }
    }

    public void HideGameOver()
    {
        if(gameOverPanel != null)
        {
            gameOverPanel.gameObject.SetActive(false);
        }
        Time.timeScale = 1f; // Resume the game
        isGameOver = false;
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f; // Resume the game
        if(MenuManager.Instance != null)
        {
            HideGameOver();
            MenuManager.Instance.PauseGame();
            GameController.Instance.RestartCurrentLevel(); 
        }
        else
        {
            Debug.LogWarning("MenuManager instance not found. Cannot return to main menu.");
        }
        
    }

}
