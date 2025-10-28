using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager instance { get; private set; }
    public DialogueEvents dialogueEvents;

    private void Awake()
    {
        if (instance != null )
        {
            Debug.LogError("Found more than one Game Events manager in the scene.");
        }
        instance = this;
        dialogueEvents = new DialogueEvents();
    }
}
