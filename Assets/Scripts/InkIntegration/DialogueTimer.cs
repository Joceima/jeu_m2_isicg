using TMPro;
using UnityEngine;

public class DialogueTimer : MonoBehaviour
{
    public static DialogueTimer instance;
    public float timeRemaining;
    public bool timerIsRunning = false;

    public TextMeshProUGUI timerText;// pour l'instant un texte pour afficher le timer puis plus tard une barre


   
    private void Awake()
    {
        instance = this;
    }

    public void StartTimer(float duration)
    {
        timeRemaining = duration;
        timerIsRunning = true;
        timerText.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (!timerIsRunning) return;

        timeRemaining -= Time.deltaTime;
        timerText.text = (Mathf.Ceil(timeRemaining)).ToString();

        if(timeRemaining <= 0)
        {
            timerIsRunning = false;
            timeRemaining = 0;
            timerText.gameObject.SetActive(false);
            DialogueManager.instance.ChooseDefaultChoice();
        }
    }
}
