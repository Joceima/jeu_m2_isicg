using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTimer : MonoBehaviour
{
    public static DialogueTimer instance;

    [Header("UI Elements")]
    public TextMeshProUGUI timerText;
    public Slider timerSlider;

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
        if (timerSlider != null)
            timerSlider.gameObject.SetActive(false);

        //if (timerText != null)
         //  timerText.gameObject.SetActive(false);
    }

    public void StartTimer(float duration)
    {
        timeRemaining = duration;
        timerIsRunning = true;

        if (timerSlider != null)
        {
            timerSlider.maxValue = duration;
            timerSlider.value = duration;
            timerSlider.gameObject.SetActive(true);
        }

        /*if (timerText != null)
        {
            timerText.gameObject.SetActive(true);
            timerText.text = Mathf.Ceil(duration).ToString();
        }*/
    }

    private void Update()
    {
        if (!timerIsRunning) return;

        timeRemaining -= Time.deltaTime;

        // Met à jour l’affichage
        if (timerSlider != null)
            timerSlider.value = timeRemaining;

        /*if (timerText != null)
            timerText.text = Mathf.Ceil(timeRemaining).ToString();*/

        // Quand le temps est écoulé
        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            timerIsRunning = false;

            if (timerSlider != null)
                timerSlider.value = 0;

            if (timerSlider != null)
                timerSlider.gameObject.SetActive(false);

            // Appelle la méthode du dialogue manager
            if (DialogueManager.instance != null)
                DialogueManager.instance.ChooseDefaultChoice();
            // ajouter une logique pour afficher directement la suite du dialogue
            // corriger le fait que la ligne du dialogue du premier choix ne s'affiche pas
        }
    }

}
