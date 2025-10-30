using UnityEngine;

public class InkTriggers : MonoBehaviour
{
    public static void ProcessTag(string tag)
    {
        switch(tag)
        {
            case "AddMotivation":
                Debug.Log("Motivation augmentée");
                // action
                break;
            case "RemoveMotivation":
                Debug.Log("Motivation diminuée");
                // action
                break;
            case "QTE_Social":
                Debug.Log("Déclenchement QTE Social");
                // action
                break;
            default:
                Debug.Log("Tag inconnu: " + tag);
                break;
        }
    }
}
