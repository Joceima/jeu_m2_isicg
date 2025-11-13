using UnityEngine;
using UnityEngine.Playables;
using System.Collections.Generic;
using System.Collections;

public class CutsceneController : MonoBehaviour
{
    public static CutsceneController Instance;

    [System.Serializable]
    public class Cutscene
    {
        public string cutsceneName;
        public PlayableDirector director;
    }

    [Header("List des Cutscenes")]
    public List<Cutscene> cutscenes = new List<Cutscene>();

    public bool isCutscenePlaying { get; private set; } = false;
    public bool isCutsceneOver { get; private set; } = false;

    public void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Update()
    {
        if (!isCutscenePlaying) return;
    }

    public IEnumerator PlayCutscene(string name)
    {
        // trouve la cutscene par son nom
        Debug.Log("Searching for cutscene: " + name);
        Cutscene currentScene = cutscenes.Find(c => c.cutsceneName == name);
   
        if(currentScene != null && currentScene.director != null)
        {
            Debug.Log("Playing cutscene : " + currentScene.cutsceneName);
            isCutscenePlaying = true;
            isCutsceneOver = false;

            currentScene.director.stopped += OnCutsceneEnded;
            currentScene.director.Play();

            //yield return new WaitForSeconds((float)currentScene.director.duration);

            yield return new WaitUntil(() => isCutsceneOver);

            isCutscenePlaying = false;
            Debug.Log("Cutscene finished: " + currentScene.cutsceneName);
            yield break;
        } else
        {

           Debug.LogWarning("Cutscene not found or director is null: " + name);
        } 
    }

    private void OnCutsceneEnded(PlayableDirector director)
    {
        isCutsceneOver = true;
        director.stopped -= OnCutsceneEnded;
        Debug.Log("Cutscene ended: " + director.gameObject.name);
    }
}
