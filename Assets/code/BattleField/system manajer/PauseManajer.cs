using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseManajer : MonoBehaviour
{
 public GameObject pauseMenuUI;
 private bool isPaused = false;


    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }    
}
