using UnityEngine;
using Ink.Runtime;
using TMPro;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{

    private static DialogueManager instance;
    [Header("Ink Story")]
    

    [Header("Dialogue UI Elements")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;

    private Story story;

    private bool dialogueIsPlaying = false;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.Log("Found more than on Dialogue Manager in the scene");
        }
        instance = this;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
    }

    private void Update()
    {
        if (!dialogueIsPlaying)
        {
            return; 
        }

        if (Keyboard.current.enterKey.wasPressedThisFrame)
        { 
            ContinueStory();
        }
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }


    public void EnterDialogueMode(TextAsset inkJSON)
    {
        if (dialoguePanel == null)
        {
            Debug.LogError("dialoguePanel is null!");
        }
        else
        {
            Debug.Log("Activating dialogue panel");
            dialoguePanel.SetActive(true);
        }
            
        story = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        ContinueStory();
    }

    private void ExitDialogueMode()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    private void ContinueStory()
    {
        if (story.canContinue)
        {
            dialogueText.text = story.Continue();
        }
        else
        {
            ExitDialogueMode();
        }
    }
}
