using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class BrainFogController : MonoBehaviour
{
    [Range(0f, 1f)] public float fogIntensity = 0f;
    public Color fogColor = new Color(0.8f, 0.8f, 0.9f);
    public float minFogDensity = 0.001f;
    public float maxFogDensity = 0.05f;

    void Start()
    {
        RenderSettings.fog = true;
        RenderSettings.fogMode = FogMode.Exponential;
    }

    private void Update()
    {
        RenderSettings.fogColor = fogColor;
        RenderSettings.fogDensity = Mathf.Lerp(minFogDensity, maxFogDensity, fogIntensity);
    }

    public void SetFogIntensity(float intensity)
    {
        fogIntensity = Mathf.Clamp01(intensity);
    }

}
