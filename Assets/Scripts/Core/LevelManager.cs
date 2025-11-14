using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public LevelManager Instance;
    [Header("Level Settings")]
    public int levelIndex;

    [Header("Cutscenes to play before level starts")]
    public int startCutsceneIndex = -1;

    [Header("Ink Dialogue JSON File")]
    public TextAsset inkJSON;

    // à décommenter plus tard pour le QTE
    /*[Header("QTE Settings")]
    public float timerDuration;
    public int QTEIndex;

    [Header("Background Music Track")]
    public AudioClip backgroundMusic;*/ 

    [Header("Cutscenes to play after level ends")]
    public int endCutsceneIndex = -1;

    private bool isRestarting = false;

    public void StartLevelSequence()
    {
        Debug.Log($"Starting Level Sequence for Level {levelIndex}");
        StartCoroutine(RunLevelSequence());
    }

    public void StopLevelSequence()
    {
        StopAllCoroutines();
        Debug.Log($"Level Sequence for Level {levelIndex} stopped.");
    }


    private IEnumerator RunLevelSequence()
    {
        Debug.Log("Starting Level Sequence for Level: " + levelIndex);
        // debug level settings
        Debug.Log("Initializing Level: " + levelIndex);
        Debug.Log("Start Cutscene Index: " + startCutsceneIndex);
        Debug.Log("Ink JSON File: " + (inkJSON != null ? inkJSON.name : "None"));
        //Debug.Log("QTE Timer Duration: " + timerDuration);
        //Debug.Log("QTE Index: " + QTEIndex);
        //Debug.Log("Background Music: " + (backgroundMusic != null ? backgroundMusic.name : "None"));
        Debug.Log("End Cutscene Index: " + endCutsceneIndex);

        if (startCutsceneIndex >=0)
        {
            string cutsceneName = "cutscene_" + startCutsceneIndex; 
            Debug.Log("Playing Start Cutscene: " + cutsceneName);
            yield return StartCoroutine(CutsceneController.Instance.PlayCutscene(cutsceneName));
        } else 
        {
            Debug.Log("No Start Cutscene to play.");
        }

        if(inkJSON != null)
        {
            if (DialogueManager.instance.dialogueIsPlaying)
            {
                DialogueManager.instance.ResetDialogue();
            }
            Debug.Log("Starting Dialogue from Ink JSON: " + inkJSON.name);
            DialogueManager.instance.PlayDialogueAutomatically(inkJSON);
            yield return new WaitUntil(() => !DialogueManager.instance.dialogueIsPlaying);
        } else
        {

           Debug.Log("No Ink JSON provided for dialogue.");
        }

        if(endCutsceneIndex >=0)
        {
            string cutsceneName = "cutscene_" + endCutsceneIndex;
           Debug.Log("Playing End Cutscene: " + cutsceneName);
           yield return StartCoroutine(CutsceneController.Instance.PlayCutscene(cutsceneName));
        }  else
        {

          Debug.Log("No End Cutscene to play.");
        }
        if (!isRestarting)
        {
            GameController.Instance.OnLevelComplete(levelIndex);
        }
        isRestarting = false;
        Debug.Log($"Level {levelIndex} complete, notifying GameController...");
        //GameController.Instance.OnLevelComplete(levelIndex);
    }
}
