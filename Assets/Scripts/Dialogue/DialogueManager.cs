using UnityEngine;
using Ink.Runtime;
using TMPro;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{

    [Header("Typing Settings")]
    [SerializeField] private float typingSpeed = 0.04f;
    
    [Header("Ink Story")]
    

    [Header("Dialogue UI Elements")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI displayNameText;
    [SerializeField] private Animator portraitAnimator;
    private Animator layoutAnimator;

    [Header("Choices UI Elements")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    private Story story;
    private static DialogueManager instance;
    public bool dialogueIsPlaying { get; private set; } // readonly 

    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";
    private const string LAYOUT_TAG = "layout";

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

        layoutAnimator = dialoguePanel.GetComponent<Animator>();

        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    private void Update()
    {
        if (!dialogueIsPlaying)
        {
            return; 
        }

        if ( Keyboard.current.enterKey.wasPressedThisFrame)
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

        displayNameText.text = "???"; // nom par défaut au début du dialogue
        portraitAnimator.Play("Default");
        layoutAnimator.Play("right");

        ContinueStory();
    }

    private IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSeconds(0.4f); // permet de s'assurer que toutes les actions de fin de dialogue sont terminées avant de fermer le panneau
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    private void ContinueStory()
    {
        if (story.canContinue)
        {
         
  
            dialogueText.text = story.Continue().Trim();
            HandleTags(story.currentTags);
            DisplayChoices();
        }
        else if (story.currentChoices.Count > 0) {
            DisplayChoices();
        }
        else
        {
            StartCoroutine(ExitDialogueMode());
        }
    }

    /*private IEnumerator DisplayLine(string line)
    {
        dialogueText.text = "";
        canContinueToNextLine = false;
        foreach (char letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        canContinueToNextLine = true;
    }*/

    private void HandleTags(List<string> currentTags)
    {

        foreach (string tag in currentTags)
        {
            string[] splitTag = tag.Split(':');
            if (splitTag.Length != 2)
            {
                Debug.LogError("Tag could not be appropriately split: " + tag);
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            switch (tagKey)
            {
                case SPEAKER_TAG:
                    Debug.Log("Speaker: " + tagValue);
                    // Gérer le changement de locuteur ici
                    displayNameText.text = tagValue;
                    break;
                case PORTRAIT_TAG:
                    Debug.Log("Portrait: " + tagValue);
                    // Gérer le changement de portrait ici
                    portraitAnimator.Play(tagValue); //
                    break;
                case LAYOUT_TAG:
                    Debug.Log("Layout: " + tagValue);
                    // Gérer le changement de mise en page ici
                    layoutAnimator.Play(tagValue);
                    break;
                default:
                    Debug.LogWarning("Unhandled tag: " + tag);
                    break;
            }
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = story.currentChoices;

        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices were given than the UI can support. Number of choices given: " + currentChoices.Count);

        }
        int index = 0;
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }
        for(int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        StartCoroutine(SelectFirstChoice());
    }

    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void MakeChoice(int choiceIndex)
    {
      
        story.ChooseChoiceIndex(choiceIndex);
        DisplayChoices();
            // problème ici
        ContinueStory();
 

    }
}
