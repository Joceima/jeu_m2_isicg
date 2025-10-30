using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    //[Header("Dialogue UI Elements")]
    //public TextMeshProUGUI dialogueText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /*public void ShowDialogue(string line)
    {
        Debug.Log("Afficher le dialogue: " + line);
        dialogueText.text = line;
    }*/
}
