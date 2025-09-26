using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using TMPro;

public class GameController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    SpawnTarget spawnTarget;

    public int score = 0; // ou créer des getter ou setter 

    [SerializeField] TMP_Text pauseText;

    int currentTargetNumber = 0;

    public float timeLimit = 2.0f;

    int gameState = 0; // 0 en cours, 1 gagné, 2 fin de jeu

    

    void Start()
    {
        spawnTarget = GetComponent<SpawnTarget>();
        spawnTarget.SpawnSomeTargets(10);
        gameState = 0;
        pauseText.text = "En cours...";
    }


    // Update is called once per frame
    void Update()
    {
        timeLimit -= Time.deltaTime;
        if(timeLimit <= 0)
        {
            gameState = 1;
            Debug.Log("Game Over! Your score: " + score);
            Time.timeScale = 0;
        }
        if(gameState == 0)
        {
            currentTargetNumber = GameObject.FindGameObjectsWithTag("Cible").Length;
            if (currentTargetNumber == 0)
            {
                spawnTarget.SpawnSomeTargets(10);
            }
        }





    }
}
