using System;
using UnityEngine;

public class QTEManager : MonoBehaviour
{
    public static QTEManager Instance;

    [Header("QTE References")]
    [SerializeField] private QTEConcentration qteConcentration;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    
    public void StartQTE(string type, float duration, int targetCount, Action<bool> onComplete = null)
    {
        Debug.Log("Appel de StartQTE dans QTEManager avec le type: " + type);
        switch (type.ToLower())
        {
            case "concentration":
                var qte = Instantiate(qteConcentration, transform);
                qte.Init(duration, success =>
                {
                    onComplete?.Invoke(success);
                    Destroy(qte.gameObject);
                });

                qte.Setup(targetCount);
                qte.StartQTE();
                break;

            default:
                Debug.LogWarning($"QTE type '{type}' not recognized.");
                onComplete?.Invoke(false);
                break;
        }
    }
}
