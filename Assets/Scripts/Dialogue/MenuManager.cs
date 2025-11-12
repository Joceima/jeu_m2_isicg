using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    [Header("Menu UI elements")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button exitButton;

    private bool isMenuOpen = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else { 
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        if (menuPanel != null)
        {
            menuPanel.SetActive(false);
        }
    }

    private void Start()
    {
        if(continueButton != null)
        {
            continueButton.onClick.AddListener(ResumeGame);
        }
        if (exitButton != null)
        {
            exitButton.onClick.AddListener(QuitGame);
        }
    }

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if(isMenuOpen)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        isMenuOpen = true;
        menuPanel.SetActive(true);
        Debug.Log("Game Paused.");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isMenuOpen = false;
        menuPanel.SetActive(false);
        Debug.Log("Game Resumed.");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }

}
