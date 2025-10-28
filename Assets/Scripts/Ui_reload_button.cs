using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Ui_reload_button : MonoBehaviour, IPointerDownHandler
{
   public void OnPointerDown(PointerEventData eventData)
    {
        if(UI.instance != null)
        {
            UI.instance.ButtonClicked();
        }
        gameObject.SetActive(false);
    }


}
