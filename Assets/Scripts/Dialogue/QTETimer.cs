using UnityEngine;
using UnityEngine.UI;

public class QTETimer : MonoBehaviour
{
    public static QTETimer instance;

    [Header("UI Elements")]
    public Slider QTESlider;

    [Header("Timer Settings")]
    public float timeRemaining;
    public bool timerIsRunning = false;

    private void Awake()
    {
        // Singleton pattern sécurisé
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        if (QTESlider != null)
            QTESlider.gameObject.SetActive(false);
    }

    public void StartQTE(float duration)
    {
        timeRemaining = duration;
        timerIsRunning = true;

        if (QTESlider != null)
        {
            QTESlider.maxValue = duration;
            QTESlider.value = duration;
            QTESlider.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if (!timerIsRunning) return;

        timeRemaining -= Time.deltaTime;

        // Met à jour l’affichage
        if (QTESlider != null)
            QTESlider.value = timeRemaining;

        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            timerIsRunning = false;
            // Appelle une méthode pour gérer la fin du QTE
            // Il faut que j'appelle une méthode pour gérer la fin du QTE 

            Debug.Log("QTE time out");
        }
    }
}

