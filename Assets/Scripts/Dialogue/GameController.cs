using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    [SerializeField] private LevelManager[] levels;
    private int currentLevelIndex = 0;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        StartLevel(0);
    }

    public void StartLevel(int levelIndex)
    {
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].gameObject.SetActive(i == levelIndex);
        }

        if (levelIndex < levels.Length)
        {
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
}
