using Ink.Parsed;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class QTEConcentration : QTEBase
{
    [Header("UI Elements")]
    [SerializeField] private GameObject qtePanel;
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private RectTransform spawnArea;
    [SerializeField] private Slider circularTimer;
    [SerializeField] private float buttonSize = 60f;
    
    private int totalTargets;
    private int clickedTargets = 0;
    private List<GameObject> activeButtons = new List<GameObject>();

    public void Setup(int targetCount)
    {
        totalTargets = targetCount;
    }

    public override void StartQTE()
    {
        Debug.Log("QTE Concentration Started");
        isRunning = true;
        clickedTargets = 0;
        qtePanel.SetActive(true);

        circularTimer.gameObject.SetActive(true);

        GenerateTargets(totalTargets);
        StartCoroutine(TimerCoroutine());
        //StartCoroutine(UpdateCircularTimer());
    }

    private void GenerateTargets(int count)
    {
        Debug.Log("Generating " + count + " targets.");
        foreach (var btn in activeButtons)
        {
            Destroy(btn);
        }
        activeButtons.Clear();
        Debug.Log("Cleared existing buttons.");

        for (int i = 0; i < count; i++)
        {
            GameObject newBtn = Instantiate(buttonPrefab, spawnArea);
            RectTransform btnRect = newBtn.GetComponent<RectTransform>();
            btnRect.sizeDelta = new Vector2(buttonSize, buttonSize);
            btnRect.anchoredPosition = new Vector2(
                Random.Range(buttonSize, spawnArea.rect.width - buttonSize),
                Random.Range(buttonSize, spawnArea.rect.height - buttonSize)
                );
            Debug.Log("Spawned button at: " + btnRect.anchoredPosition);

            Button b = newBtn.GetComponent<Button>();
            b.onClick.AddListener(() => OnTargetClicked(newBtn));

            circularTimer.maxValue = duration;
            circularTimer.value = duration;

            StartCoroutine(UpdateCircularTimer());
            activeButtons.Add(newBtn);
            Debug.Log("Button added to active list.");
        }
    }

    private void OnTargetClicked(GameObject button)
    {
        Debug.Log("Target Clicked");
        clickedTargets++;
        activeButtons.Remove(button);
        Destroy(button);
        if (clickedTargets >= totalTargets)
        {
            EndQTE(true);
        }
    }

    private IEnumerator UpdateCircularTimer()
    {
        Debug.Log("Starting Circular Timer Update");
        circularTimer.maxValue = duration;
        circularTimer.value = duration;
        while (isRunning && circularTimer.value > 0)
        {
            circularTimer.value -= Time.deltaTime;
            yield return null;
        }
    }

    public override void EndQTE(bool success)
    {
        if(!isRunning) return;

        isRunning = false;
        foreach(var btn in activeButtons)
        {
            Destroy(btn);
        }
        activeButtons.Clear();

        circularTimer.gameObject.SetActive(false);
        qtePanel.SetActive(false);
        onQTEComplete?.Invoke(success);
    }
}
