using UnityEngine;
using UnityEngine.UI;

public class MotivationManager : MonoBehaviour
{
    public static MotivationManager instance;

    [Header("Motivation Settings")]
    [SerializeField] private Slider MotivationBar;
    [SerializeField] private float maxMotivation = 100f;
    [SerializeField] private float minMotivation = 0f;

    [Header("Current State")]
    [SerializeField] private float currentMotivation = 50f;

    private void Awake()
    {
        // Singleton pattern sécurisé
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    private void Start()
    {
        if (MotivationBar != null)
        {
            MotivationBar.minValue = minMotivation;
            MotivationBar.maxValue = maxMotivation;
            MotivationBar.value = currentMotivation;
        }
    }

    public void AddMotivation(float amount)
    {
        currentMotivation = Mathf.Clamp(currentMotivation + amount, minMotivation, maxMotivation);
        UpdateSociabilityBar();
    }

    public void RemoveSociability(float amount)
    {
        currentMotivation = Mathf.Clamp(currentMotivation - amount, minMotivation, maxMotivation);
        UpdateSociabilityBar();
    
    }

    private void UpdateSociabilityBar()
    {
        if (MotivationBar != null)
            MotivationBar.value = currentMotivation;
    }
}
