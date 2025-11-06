using UnityEngine;
using UnityEngine.Playables;
using System.Collections.Generic;

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

    public void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void PlayCutscene(string name)
    {
        // trouve la cutscene par son nom
        Cutscene curentScene = cutscenes.Find(c => c.cutsceneName == name);
        if (curentScene != null)
        {
            Debug.Log("Playing cutscene: " + name);
            isCutscenePlaying = true;
            curentScene.director.Play();
            curentScene.director.stopped += OnCutsceneEnded;
        }
        else
        {
            Debug.LogWarning("Cutscene not found: " + name);
        }
    }

    private void OnCutsceneEnded(PlayableDirector director)
    {
        isCutscenePlaying = false;
        director.stopped -= OnCutsceneEnded;
        Debug.Log("Cutscene ended: " + director.gameObject.name);
    }
}
