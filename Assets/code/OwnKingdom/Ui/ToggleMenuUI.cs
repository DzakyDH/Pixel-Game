using UnityEngine;
using UnityEngine.SceneManagement;

public class ToggleMenuUI : MonoBehaviour
{
    public GameObject MenuPanel;

    private bool isMenuVisible;

    public void ToggleMenu()
    {
        isMenuVisible = !isMenuVisible;
        MenuPanel.SetActive(isMenuVisible);
    }
    public void CloseMenu()
    {
        isMenuVisible = false;
        MenuPanel.SetActive(false);
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
