using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("BattleField");
    }

    // Update is called once per frame
    public void QuitGame()
    {
        Application.Quit();
    }
}
