using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InstantiateTester : MonoBehaviour
{
    [Header("References")]
    public GameObject buttonPrefab;      // Ton prefab UI (doit avoir RectTransform+Image+Button)
    public RectTransform spawnArea;      // Zone UI parent (RectTransform)
    public Canvas targetCanvas;          // (optionnel) Canvas parent

    void Start()
    {
        StartCoroutine(RunTests());
    }

    private IEnumerator RunTests()
    {
        Debug.Log("=== InstantiateTester start ===");

        if (buttonPrefab == null) { Debug.LogError("Prefab non assigné !"); yield break; }
        if (spawnArea == null) { Debug.LogError("SpawnArea non assignée !"); yield break; }

        // 1) Instantiate(prefab, parent) — simple
        {
            Debug.Log("--- Test 1: Instantiate(prefab, parent) ---");
            GameObject go = Instantiate(buttonPrefab, spawnArea);
            go.name = "Inst_test1";
            RectTransform rt = go.GetComponent<RectTransform>();
            if (rt != null) rt.anchoredPosition = Vector2.zero;
            Debug.Log($"Created {go.name} parent={go.transform.parent.name} active={go.activeInHierarchy}");
            yield return new WaitForSeconds(0.2f);
        }

        // 2) Instantiate then SetParent(false)
        {
            Debug.Log("--- Test 2: Instantiate(prefab) then SetParent(parent, false) ---");
            GameObject go = Instantiate(buttonPrefab);
            go.name = "Inst_test2";
            go.transform.SetParent(spawnArea, false); // worldPositionStays = false => keeps rect transform local
            RectTransform rt = go.GetComponent<RectTransform>();
            if (rt != null) rt.anchoredPosition = new Vector2(50, 50);
            Debug.Log($"Created {go.name} parent={go.transform.parent.name} active={go.activeInHierarchy}");
            yield return new WaitForSeconds(0.2f);
        }

        // 3) Instantiate(prefab, parent.transform) with explicit false (C# overload)
        {
            Debug.Log("--- Test 3: Instantiate(prefab, parent.transform) then SetParent with false ---");
            GameObject go = Instantiate(buttonPrefab, spawnArea.transform);
            go.name = "Inst_test3";
            go.transform.SetParent(spawnArea, false);
            RectTransform rt = go.GetComponent<RectTransform>();
            if (rt != null) rt.anchoredPosition = new Vector2(-50, 50);
            Debug.Log($"Created {go.name} parent={go.transform.parent.name} active={go.activeInHierarchy}");
            yield return new WaitForSeconds(0.2f);
        }

        // 4) Create from scratch (no prefab) : GameObject + UI components
        {
            Debug.Log("--- Test 4: Create GameObject and add UI components at runtime ---");
            GameObject go = new GameObject("Inst_test4", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(Button));
            go.transform.SetParent(spawnArea, false);
            RectTransform rt = go.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(80, 80);
            rt.anchoredPosition = new Vector2(0, -50);

            Image img = go.GetComponent<Image>();
            img.color = Color.cyan;

            Button btn = go.GetComponent<Button>();
            btn.onClick.AddListener(() => Debug.Log("Runtime-created button clicked"));

            Debug.Log($"Created {go.name} parent={go.transform.parent.name} active={go.activeInHierarchy}");
            yield return new WaitForSeconds(0.2f);
        }

        Debug.Log("=== InstantiateTester finished ===");
    }
}

