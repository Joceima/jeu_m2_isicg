using UnityEngine;
using UnityEngine.UI;

public class FogBlurController : MonoBehaviour
{
    [SerializeField] private float maxBlur = 5f;
    [SerializeField] private float blurSpeed = 1f;

    private Material blurMaterial;
    private float targetBlur = 0f;
    private float currentBlur = 0f;

    private void Start()
    {
        RawImage img = GetComponent<RawImage>();
        blurMaterial = Instantiate(img.material);
        img.material = blurMaterial;
    }

    private void Update()
    {
        if (blurMaterial == null) return;
        
        currentBlur = Mathf.Lerp(currentBlur, targetBlur, Time.deltaTime * blurSpeed);
        blurMaterial.SetFloat("_Size", currentBlur);
    }

    public void SetFogLevel(float level)
    {
        targetBlur = Mathf.Clamp01(level) * maxBlur;
    }
}
