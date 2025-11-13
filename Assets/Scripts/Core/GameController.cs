using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    [SerializeField] public LevelManager[] levels;
    public int currentLevelIndex;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
            return;
        }
        
    }

    private void Start()
    {
        currentLevelIndex = 0;
        StartLevel(currentLevelIndex);
    }

    public void StartLevel(int levelIndex)
    {
        for (int i = 0; i < levels.Length; i++)
        {
            if (levels[i] != null)
                levels[i].gameObject.SetActive(i == levelIndex);
        }

        if (levelIndex < levels.Length && levels[levelIndex] != null)
        {
            currentLevelIndex = levelIndex;
            levels[levelIndex].StartLevelSequence();
        }
    }


    public void OnLevelComplete(int levelIndex)
    {
        Debug.Log("Level " + levelIndex + " completed.");
        currentLevelIndex++;
        if(currentLevelIndex < levels.Length)
        {
            StartLevel(currentLevelIndex);
        } else
        {
            Debug.Log("All levels completed! Game Over.");
        }
    }

    public void RestartCurrentLevel()
    {
        Debug.Log("Restarting Level: " + currentLevelIndex);
        if(GameOverManager.Instance != null)
        {
            GameOverManager.Instance.HideGameOver();
        }
        if(MotivationManager.instance != null)
        {
            MotivationManager.instance.ResetMotivation();
        }

        if(DialogueManager.instance != null)
        {
            DialogueManager.instance.ResetDialogue();
        }
        //levels[currentLevelIndex].StopAllCoroutines();
        //levels[currentLevelIndex].StartLevelSequence();
        foreach(var level in levels)
        {
            if(level != null)
            {
                level.StopAllCoroutines();
                level.gameObject.SetActive(false);
            }
        }
        StartLevel(currentLevelIndex);
    }

    public void StopLevelSequence()
    {
        StopAllCoroutines();
    }
}
