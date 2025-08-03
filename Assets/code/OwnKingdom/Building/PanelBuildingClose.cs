using UnityEngine;
using UnityEngine.EventSystems;

public class PanelBuildingClose : MonoBehaviour, IPointerDownHandler
{
    public GameObject panelToClose;
    public GameObject ButtonTOOpen;

    public void OnPointerDown(PointerEventData eventData)
    {
        if(panelToClose != null && panelToClose.activeSelf)
        {
            panelToClose.SetActive(false);
            ButtonTOOpen.SetActive(true);
        }
    }
}
