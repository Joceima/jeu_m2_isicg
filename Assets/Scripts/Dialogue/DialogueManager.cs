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
    [SerializeField] private GameObject continueIcon;

    private Animator layoutAnimator;

    [Header("Choices UI Elements")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    [Header("Auto Dialogue Settings")]
    [SerializeField] private bool autoDialogueMode = false;  // Active ou non le mode auto
    [SerializeField] private float autoDialogueDelay = 2f;   // Temps avant ligne suivante

    private Story story;

    //private static DialogueManager instance;
    public static DialogueManager instance; // pas sûre que je mette dialogue manager en singleton mais pour l'instant ça me semble utile
    public bool dialogueIsPlaying = false; // readonly removed  

    public bool canContinueToNextLine = false;
    private Coroutine displayLineCoroutine;
    public bool isTyping = false;

    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";
    private const string LAYOUT_TAG = "layout";
    private const string TIMER_TAG = "timer";
    private const string QTE_TAG = "QTEConcentration";
    private const string RESPONSE_TAG = "response";

    private void Awake()
    {
        if(instance != null)
        {
            Debug.Log("Found more than on Dialogue Manager in the scene");
        }
        instance = this;
    }

    public void PlayDialogueAutomatically(TextAsset inkJSON)
    {
        autoDialogueMode = true;
        EnterDialogueMode(inkJSON); 
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
        if (!dialogueIsPlaying || autoDialogueMode) return;

        if (canContinueToNextLine && Keyboard.current.enterKey.wasPressedThisFrame)
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
        if (isTyping) return;

        if (story.canContinue)
        {
            if (displayLineCoroutine != null)
                StopCoroutine(displayLineCoroutine);

            string nextLine = story.Continue();
            HandleTags(story.currentTags);
            displayLineCoroutine = StartCoroutine(DisplayLine(nextLine.Trim()));
        }
        else if (story.currentChoices.Count > 0)
        {
            DisplayChoices();
        }
        else
        {
            autoDialogueMode = false;
            StartCoroutine(ExitDialogueMode());
        }
    }


    private IEnumerator AutoContinueAfterDelay()
    {
        yield return new WaitForSeconds(autoDialogueDelay);
        ContinueStory();
    }


    private IEnumerator DisplayLine(string line)
    {
        dialogueText.text = "";
        continueIcon.SetActive(false);
        HideChoices();
        canContinueToNextLine = false;
        isTyping = true;

        foreach (char letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
        continueIcon.SetActive(true);
        DisplayChoices();
        
        canContinueToNextLine = true;

        if (autoDialogueMode && story.currentChoices.Count == 0)
        {
            yield return new WaitForSeconds(autoDialogueDelay);
            ContinueStory();
        }
    }


    private void HideChoices()
    {
        foreach (GameObject choice in choices)
        {
            choice.SetActive(false);
        }
    }

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
                case TIMER_TAG:
                    Debug.Log("Timer: " + tagValue);
                    Debug.Log("Timer value as float: " + float.Parse(tagValue));
                    // Gérer le timer ici
                    float time = float.Parse(tagValue);
                    DialogueTimer.instance.StartTimer(time);
                    break;
                case QTE_TAG:
                    Debug.Log("QTE: " + tagValue);
                    // Gérer le QTE ici
                    string[] qteParams = tagValue.Split(',');
                    if (qteParams.Length != 2)
                    {
                        Debug.LogError("QTE tag parameters are incorrect: " + tagValue);
                        break;
                    }
                    float qteDuration = float.Parse(qteParams[0].Trim());
                    int qteTargetCount = int.Parse(qteParams[1].Trim());
                    // peut être mettre le dialogue en pause

                    QTEManager.Instance.StartQTE("concentration", qteDuration, qteTargetCount, success =>
                    {
                        if(success)
                            {
                            Debug.Log("QTE succeeded!");
                        } else
                        {
                            Debug.Log("QTE failed.");
                        }
                        // suite du dialogue après le QTE ?
                    });
                    break;
                case RESPONSE_TAG:
                    Debug.Log("Response: " + tagValue);
                    if (tagValue.ToLower() == "bad")
                        MotivationManager.instance.RemoveMotivation(10);
                    else if (tagValue.ToLower() == "good")
                        MotivationManager.instance.AddMotivation(30);
                    break;

                default:
                    Debug.LogWarning("Unhandled tag: " + tag);
                    break;
            }
        }
    }


    public void ChooseDefaultChoice()
    {
        if(story.currentChoices.Count > 0)
        {
            story.ChooseChoiceIndex(story.currentChoices.Count - 1);
            ContinueStory();
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
        if(canContinueToNextLine)
        {
            story.ChooseChoiceIndex(choiceIndex);
            DisplayChoices();
            // problème ici
            ContinueStory();
        }
    }


    public void ResetDialogue()
    {
        Debug.Log("Resetting dialogue manager state.");
        dialogueIsPlaying = false;
        story = null;
        StopAllCoroutines();
        if(dialoguePanel != null)
            dialoguePanel.SetActive(false);
    }
}
