using UnityEngine;

public class PanelSwicther : MonoBehaviour
{
    public GameObject panelA;
    public GameObject panelB;

    public void ShowPanelA()
    {
        panelA.SetActive(true);
        panelB.SetActive(false);
    }
    public void ShowPanelB()
    {
        panelA.SetActive(false);
        panelB.SetActive(true);
    }
}
