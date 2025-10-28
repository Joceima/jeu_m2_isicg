using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Ui_reload_button : MonoBehaviour, IPointerDownHandler
{
   public void OnPointerDown(PointerEventData eventData)
    {
        //UI.instance.reloadSteps--;
        /*if (UI.instance.reloadSteps == 0)
        {
            // All steps completed
            Debug.Log("Reload complete!");
        }*/
        gameObject.SetActive(false);
    }
}
