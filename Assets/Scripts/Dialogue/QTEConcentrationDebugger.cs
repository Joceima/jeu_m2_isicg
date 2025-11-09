using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[ExecuteInEditMode]
public class QTEConcentrationDebugger : MonoBehaviour
{
    [Header("Références")]
    public QTEConcentration qteConcentration;

    [ContextMenu("?? Vérifier la configuration du QTE")]
    public void RunDiagnostics()
    {
        if (qteConcentration == null)
        {
            Debug.LogError("? Aucun QTEConcentration assigné !");
            return;
        }

        Debug.Log("===== ?? Diagnostic QTEConcentration =====");

        bool allGood = true;

        // --- Vérif du prefab
        var prefab = GetPrivateField<GameObject>(qteConcentration, "buttonPrefab");
        if (prefab == null)
        {
            Debug.LogError("? buttonPrefab n’est pas assigné dans l’inspector !");
            allGood = false;
        }
        else
        {
            Debug.Log("? buttonPrefab trouvé : " + prefab.name);
            if (prefab.GetComponent<RectTransform>() == null)
            {
                Debug.LogError("? buttonPrefab n’a PAS de RectTransform !");
                allGood = false;
            }
            if (prefab.GetComponentInChildren<Button>() == null)
            {
                Debug.LogError("? buttonPrefab n’a PAS de composant Button !");
                allGood = false;
            }
        }

        // --- Vérif du qtePanel
        var qtePanel = GetPrivateField<GameObject>(qteConcentration, "qtePanel");
        if (qtePanel == null)
        {
            Debug.LogError("? qtePanel non assigné !");
            allGood = false;
        }
        else
        {
            Debug.Log("? qtePanel trouvé : " + qtePanel.name);
            Debug.Log("   ? Actif ? " + qtePanel.activeSelf);
        }

        // --- Vérif du spawnArea
        var spawnArea = GetPrivateField<RectTransform>(qteConcentration, "spawnArea");
        if (spawnArea == null)
        {
            Debug.LogError("? spawnArea non assigné !");
            allGood = false;
        }
        else
        {
            Debug.Log($"? spawnArea trouvé ({spawnArea.name}) ? Taille: {spawnArea.rect.width}x{spawnArea.rect.height}");
            Debug.Log("   ? Actif ? " + spawnArea.gameObject.activeSelf);
        }

        // --- Vérif du circularTimer
        var circularTimer = GetPrivateField<Slider>(qteConcentration, "circularTimer");
        if (circularTimer == null)
        {
            Debug.LogWarning("?? circularTimer n’est pas assigné (pas bloquant mais recommandé).");
        }
        else
        {
            Debug.Log("? circularTimer trouvé : " + circularTimer.name);
        }

        if (allGood)
        {
            Debug.Log("?? Tous les éléments essentiels sont correctement assignés !");
        }
        else
        {
            Debug.LogWarning("?? Des éléments manquent ou sont mal configurés — voir les ? ci-dessus.");
        }

        Debug.Log("===========================================");
    }

    // --- Utilitaire : accès à un champ privé dans QTEConcentration
    private T GetPrivateField<T>(object obj, string fieldName)
    {
        var field = obj.GetType().GetField(fieldName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        if (field == null) return default;
        return (T)field.GetValue(obj);
    }
}

