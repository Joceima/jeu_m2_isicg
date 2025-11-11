using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class MotivationManager : MonoBehaviour
{
    public static MotivationManager instance;

    [Header("Motivation Settings")]
    [SerializeField] private Slider MotivationBar;
    [SerializeField] private float maxMotivation = 100f;
    [SerializeField] private float minMotivation = 0f;



    [Header("Current State")]
    [SerializeField] private float currentMotivation = 100f;
    [SerializeField] private Image fillImage;
    [SerializeField] private Gradient motivationGradient;
    private float startingMotivation;

    [Header("Volume blur Settings")]
    [SerializeField] private Volume globalVolume;

    [Header("Decay Settings")]
    [SerializeField] private float decayRate = 0.2f; // Motivation decay per second
    [SerializeField] private bool autoDecayEnabled = true;

    private Bloom bloom;
    private ColorAdjustments colorAdjustments;
    private Vignette vignette;

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
        startingMotivation = currentMotivation;
        if (MotivationBar != null)
        {
            MotivationBar.minValue = minMotivation;
            MotivationBar.maxValue = maxMotivation;
            MotivationBar.value = currentMotivation;
        }

        if(globalVolume.profile.TryGet(out bloom))
        {
            bloom.active = true;
        }

        if(globalVolume.profile.TryGet(out colorAdjustments))
        {
            colorAdjustments.active = true;
        }
        if(globalVolume.profile.TryGet(out vignette))
        {
            vignette.active = true;
        }
    }

    private void Update()
    {
     
        if (autoDecayEnabled)
        {
            DecayOverTime();
        }
        if (fillImage != null && motivationGradient != null)
        {
            float t = currentMotivation / maxMotivation;
            fillImage.color = motivationGradient.Evaluate(t);
        }
        UpdateVisualEffects();
      
    }

    private void UpdateVisualEffects()
    {
        float factor = 1f;
        if(currentMotivation < 50f)
        {
            factor = 1.5f;
        }
        float fogIntensity = (1f - (currentMotivation / maxMotivation))*factor;
        if (bloom != null)
        {
            //Debug.Log("Updating bloom intensity to " + Mathf.Lerp(5f, 20f, fogIntensity));
            bloom.intensity.value = Mathf.Lerp(5f, 20f, fogIntensity);
        }
        if (colorAdjustments != null)
        {
            //Debug.Log("Updating color adjustments saturation to " + Mathf.Lerp(0f, -100f, fogIntensity) + " and contrast to " + Mathf.Lerp(0f, 50f, fogIntensity));
            colorAdjustments.saturation.value = Mathf.Lerp(0f, -100f, fogIntensity);
            colorAdjustments.contrast.value = Mathf.Lerp(0f, 50f, fogIntensity);
        }
        if (vignette != null)
        {
            //Debug.Log("Updating vignette intensity to " + 0.5f * Mathf.Lerp(0.3f, 0.6f, fogIntensity));
            vignette.intensity.value = 1.1f * Mathf.Lerp(0.3f, 0.6f, fogIntensity);
        }
    }




    private void DecayOverTime()
    {
        if (currentMotivation > minMotivation)
        {
            RemoveMotivation(decayRate * Time.deltaTime);
        }
    }


    public void AddMotivation(float amount)
    {
        currentMotivation = Mathf.Clamp(currentMotivation + amount, minMotivation, maxMotivation);
        UpdateMotivationBar();
        //UpdateFog();
    }

    public void RemoveMotivation(float amount)
    {
        currentMotivation = Mathf.Clamp(currentMotivation - amount, minMotivation, maxMotivation);
        UpdateMotivationBar();
        //UpdateFog();
        if (currentMotivation <= minMotivation)
        {
            GameOverManager.Instance.TriggerGameOver();
        }

    }

    private void UpdateMotivationBar()
    {
        if (MotivationBar != null)
            MotivationBar.value = currentMotivation;
    }

    public void ResetMotivation()
    {
        currentMotivation = startingMotivation;
        UpdateMotivationBar();
        UpdateVisualEffects();
    }

   /* private void UpdateFog()
    {
        if(fogController == null) return;
        float fogLevel = 1f - (currentMotivation / maxMotivation);
        //fogController.SetFogIntensity(fogLevel);
    }*/
}
