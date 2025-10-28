using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public static UI instance;

    [Header("Reload details")]
    public int reloadSteps;
    public Ui_reload_button[] reloadButtons;

    [Header("QTE Timer")]
    public float qteDuration = 3f; // Durée totale du QTE
    private float qteTimer;
    private bool qteActive = false;

    [Header("Timer Text")]
    public TMP_Text timerText; 

    private void Start()
    {
        instance = this;
        reloadButtons = GetComponentsInChildren<Ui_reload_button>(true);
    }

    private void Update()
    {
        if(Keyboard.current.rKey.wasPressedThisFrame)
        {
            OpenReloadUI();
        }
        if (qteActive)
        {
            qteTimer -= Time.deltaTime;

            if (timerText != null)
                timerText.text = $"Temps : {qteTimer:F1}s";

            if (qteTimer <= 0)
            {
                QTEFailed();
            }
        }
    }

    public void OpenReloadUI()
    {
        foreach (Ui_reload_button button in reloadButtons)
        {
            button.gameObject.SetActive(true);
            float randomX = Random.Range(200, 500f);
            float randomY = Random.Range(200, 500f);
            button.transform.position = new Vector3( randomX, randomY, 0);
        }

        //reloadSteps = reloadButtons.Length;
        reloadSteps = reloadButtons.Length;

        qteTimer = qteDuration;
        qteActive = true;
    }
     
    public void ButtonClicked()
    {
        reloadSteps--;

        if (reloadSteps <= 0 && qteActive)
        {
            QTESuccess();
        }
    }

    private void QTESuccess()
    {
        qteActive = false;
        HideAllButtons();
        if (timerText != null)
            timerText.text = "Réussi !";
        Debug.Log("QTE réussi !");
    }

    private void QTEFailed()
    {
        qteActive = false;
        HideAllButtons();
        if (timerText != null)
            timerText.text = " Raté !";
        Debug.Log("QTE raté !");
    }

    private void HideAllButtons()
    {
        foreach (Ui_reload_button button in reloadButtons)
            button.gameObject.SetActive(false);
    }
}
