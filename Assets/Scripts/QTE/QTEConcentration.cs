using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class QTEConcentration : QTEBase
{
    [Header("UI Elements")]
    [SerializeField] private GameObject qtePanel;          // Le panel global de la QTE
    [SerializeField] private GameObject buttonPrefab;       // Le prefab du bouton (avec une Image + Button + Slider dedans)
    [SerializeField] private RectTransform spawnArea;       // Zone où les boutons apparaissent
    [SerializeField] private float buttonSize = 80f;        // Taille du bouton
    [SerializeField] private float timePerButton = 6f;      // Durée avant disparition du bouton

    private int totalTargets;
    private int clickedTargets = 0;
    private List<GameObject> activeButtons = new List<GameObject>();

    private bool layoutReady = false;

    private void Awake()
    {
        if (qtePanel != null)
            qtePanel.gameObject.SetActive(false);
    }

    public void Setup(int targetCount)
    {
        totalTargets = targetCount;
    }

    public override void StartQTE()
    {
        Debug.Log("QTE Concentration Started");
        isRunning = true;
        clickedTargets = 0;

        qtePanel.gameObject.SetActive(true);
        StartCoroutine(GenerateAfterLayout(totalTargets));
        StartCoroutine(TimerCoroutine());
    }

    private IEnumerator GenerateAfterLayout(int count)
    {
        Debug.Log($"SpawnArea activeInHierarchy: {spawnArea.gameObject.activeInHierarchy}");
        if(buttonPrefab == null || spawnArea == null)
        {
            Debug.LogError("Button Prefab or Spawn Area is not assigned.");
            yield break;
        }
        // Attendre un frame pour être sûr que le layout est prêt
        yield return null;

        float width = spawnArea.rect.width;
        float height = spawnArea.rect.height;
        Debug.Log($"Spawn Area Size: {width}x{height}");

        // Supprimer les anciens boutons s'il y en a
        foreach (var btn in activeButtons)
            Destroy(btn);
        activeButtons.Clear();

        for (int i = 0; i < count; i++)
        {
            // Créer le bouton
            
            GameObject newBtn = Instantiate(buttonPrefab, spawnArea);
            Image img = newBtn.GetComponentInChildren<Image>();
            if(img != null ) img.color = Color.red; // Juste pour s'assurer qu'on voit le bouton
            Debug.Log($"Created new button: {newBtn.name} (parent: {newBtn.transform.parent.name}");
            Debug.Log($"Button activeInHierarchy: {newBtn.activeInHierarchy}, activeSelf: {newBtn.activeSelf}");
            RectTransform btnRect = newBtn.GetComponent<RectTransform>();
            btnRect.sizeDelta = new Vector2(buttonSize, buttonSize);

            // Position aléatoire dans la zone
            Vector2 randomPos = new Vector2(
                Random.Range( buttonSize / 2, width - buttonSize / 2),
                Random.Range( buttonSize / 2, height - buttonSize / 2)
            );

            btnRect.anchoredPosition = randomPos - new Vector2(width / 2, height / 2);

            Debug.Log($"Spawned Button at: {btnRect.anchoredPosition}");

            // Activer le bouton
            newBtn.gameObject.SetActive(true);



            // Associer l’événement de clic

            Button b = newBtn.GetComponentInChildren<Button>();
            Debug.Log($"New Button created: {newBtn.name}, Active: {newBtn.activeSelf}");
            b.onClick.AddListener(() => OnTargetClicked(newBtn));

            // Récupérer le slider enfant pour le timer individuel
            Slider localSlider = newBtn.GetComponentInChildren<Slider>();
            if (localSlider != null)
            {
                localSlider.maxValue = timePerButton;
                localSlider.value = timePerButton;
                StartCoroutine(UpdateLocalCircularTimer(localSlider, newBtn));
            }

            activeButtons.Add(newBtn);
        }
    }

    private IEnumerator UpdateLocalCircularTimer(Slider slider, GameObject button)
    {
        float timeLeft = timePerButton;

        while (isRunning && timeLeft > 0f)
        {
            timeLeft -= Time.deltaTime;
            if (slider != null)
                slider.value = timeLeft;
            yield return null;
        }

        // Si le temps est écoulé et que le bouton est encore actif, on le détruit
        if (isRunning && button != null)
        {
            Debug.Log("Button expired");
            activeButtons.Remove(button);
            Destroy(button);

            // Si tous les boutons ont disparu => échec
            if (activeButtons.Count == 0)
                EndQTE(false);
        }
    }

    private void OnTargetClicked(GameObject button)
    {
        Debug.Log("Target Clicked");
        clickedTargets++;
        activeButtons.Remove(button);
        Destroy(button);

        if (clickedTargets >= totalTargets)
            EndQTE(true);
    }

    public override void EndQTE(bool success)
    {
        Debug.Log($"EndQTE() called with success={success}, activeButtons count={activeButtons.Count}");
        if (!isRunning) return;

        isRunning = false;

        foreach (var btn in activeButtons)
            if (btn != null)
                Destroy(btn);
        activeButtons.Clear();

        qtePanel.gameObject.SetActive(false);
        onQTEComplete?.Invoke(success);

        if(success)
            MotivationManager.instance.AddMotivation(20f);
        else 
            MotivationManager.instance.RemoveMotivation(10f);
    }
}