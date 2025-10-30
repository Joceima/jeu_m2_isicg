using UnityEngine;
using Ink.Runtime;
using System.Collections.Generic;
using System.Collections;
using TMPro;

public class InkManager : MonoBehaviour
{
    public static InkManager Instance;
    private Story story;


    [Header("Param")]
    [SerializeField] private float typingSpeed = 0.07f;

    [Header("Dialogue")]
    [SerializeField] TextAsset inkJSONAsset;
    [SerializeField] TextMeshProUGUI dialogueText;


    public bool dialogueIsPlaying { get; private set;}
    private Coroutine displayLineCoroutine;
    //private bool canContinueToNextLine = false;
    private void Awake()
    {
        Debug.Log("Awake InkManager");
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }

        if(inkJSONAsset != null)
        {
            story = new Story(inkJSONAsset.text);
            Debug.Log("Ink story loaded successfully.");
        } else
        {
             Debug.LogError("Ink JSON file is not assigned in the InkManager.");
        }
    }

    public void StartStory()
    {
        Debug.Log("Démarrage de l'histoire ink");
        if (story == null)
        {
            Debug.LogError("L'histoire ink n'est pas chargée correctement.");
            return;
        }
        ContinueStory();
    }

    private void Start()
    {
        Debug.Log("Start InkManager");
        story.ChoosePathString("main");
        StartStory();
    }


    public void ContinueStory()
    {
        if (story.canContinue)
        {
            //string text = story.Continue(); // la ligne suivant à display
            if(displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine);
            }
            displayLineCoroutine = StartCoroutine(DisplayLine(story.Continue()));
            //ShowDialogue(text);
            //Debug.Log("Ink Story Continued: " + text);

            if (story.currentTags.Count > 0)
            {
                Debug.Log("Tags trouvés: " + string.Join(", ", story.currentTags));
                ProcessTags(story.currentTags);
            }
        }
        else
        {
            Debug.Log("Fin de l'histoire ink");
        }
    }

    private IEnumerator DisplayLine(string line)
    {
        dialogueText.text = "";

        foreach(char letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed); // délai entre chaque lettre
        }
    }

    private void ProcessTags(List<string> tags)
    {
        foreach(string tag in tags) {
            Debug.Log("Tag Ink: " + tag);
            InkTriggers.ProcessTag(tag);
        }
    }

}
