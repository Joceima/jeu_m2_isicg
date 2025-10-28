using UnityEngine;
using Ink.Runtime;

public class DialogueManager : MonoBehaviour
{
    [Header("Ink Story")]

    [SerializeField] private TextAsset inkJson;

    private Story story;

    private bool dialoguePlaying = false;

    private void Awake()
    {
        story = new Story(inkJson.text);
    }
    private void OnEnable()
    {
        GameEventsManager.instance.dialogueEvents.onEnterDialogue += EnterDialogue;

    }

    private void OnDisable()
    {
        GameEventsManager.instance.dialogueEvents.onEnterDialogue -= EnterDialogue;
    }
    
    private void EnterDialogue(string knotName)
    {
        if (dialoguePlaying) // on entre dans le dialogue une fois 
        {
            return;
        }
        dialoguePlaying = true;
        if (!knotName.Equals(""))
        {
            story.ChoosePathString(knotName);
        }
        else
        {
            Debug.Log("Knot name was the empty string when entering dialogue");
        }

        ContinueOrExitStory();
    }

    private void ContinueOrExitStory()
    {
        if(story.canContinue)
        {
            string dialogueLine = story.Continue();
            Debug.Log(dialogueLine);
       
        }
        else
        {
            ExitDialogue();
            Debug.Log("Dialogue ended.");
            
        }
    }

    private void ExitDialogue()
    {
         Debug.Log("Exiting dialogue...");
        dialoguePlaying = false;

        story.ResetState();
    }
}
