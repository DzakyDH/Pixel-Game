using UnityEngine;
using UnityEngine.EventSystems;

public class PanelBuildingClose : MonoBehaviour, IPointerDownHandler
{
    public GameObject panelTooClose;
    public GameObject ButtonTOOpen;
    public GameObject PanelClose;

    public void OnPointerDown(PointerEventData eventData)
    {
        if(panelTooClose != null && panelTooClose.activeSelf)
        {
            PanelClose .SetActive(false);
            ButtonTOOpen.SetActive(true);
        }
    }
}
