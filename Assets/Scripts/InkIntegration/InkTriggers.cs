using NUnit.Framework.Internal;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InkTriggers : MonoBehaviour
{

    [Header("Visual Cue")]
    [SerializeField] TextMeshPro TextMeshVisualCue;
    private bool playerInRange = false;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;
  
    public static void ProcessTag(string tag)
    {
        switch(tag)
        {
            case "AddMotivation":
                Debug.Log("Motivation augmentée");
                // action
                break;
            case "RemoveMotivation":
                Debug.Log("Motivation diminuée");
                // action
                break;
            case "QTE_Social":
                Debug.Log("Déclenchement QTE Social");
                // action
                break;
            default:
                Debug.Log("Tag inconnu: " + tag);
                break;
        }
    }

    private void Awake()
    {
        Debug.Log("Awake InkTriggers");
        playerInRange = false;
        TextMeshVisualCue.gameObject.SetActive(false);
    }

    private void Update()
    {
        Debug.Log("Update InkTriggers playerInranger" + playerInRange);
        if (playerInRange)
        {
            Debug.Log("Player in range - showing visual cue");
            TextMeshVisualCue.gameObject.SetActive(true);
            if(Keyboard.current.eKey.wasPressedThisFrame)
            {
                Debug.Log("E key pressed - Starting Ink story");
                Debug.Log(inkJSON.text);
            }
        }
        else
        {
            //Debug.Log("Player out of range - hiding visual cue");
            TextMeshVisualCue.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Trigger entered by: " + collider.gameObject.name);
        if(collider.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if(collider.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
