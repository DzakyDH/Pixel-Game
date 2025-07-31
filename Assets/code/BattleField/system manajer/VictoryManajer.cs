using UnityEngine;
using TMPro;

public class VictoryManajer : MonoBehaviour
{
    public static VictoryManajer instance;

    public TMP_Text enemyKilledText;
    public TMP_Text allyKilledText;

    private int enemyKilledCount = 0;
    private int allyKilledCount = 0;

    public GameObject VictoryPanel;

    private void Awake()
    {
        instance = this;
        VictoryPanel.SetActive(false);

        if (enemyKilledText != null) enemyKilledText.text = "0";
        if (allyKilledText != null) allyKilledText.text = "0";
    }

    public void registerEnemyDeath()
    {
        enemyKilledCount++;
        if (enemyKilledText != null)
            enemyKilledText.text = enemyKilledCount.ToString();

        CheckEnemyRemaining();
    }

    public void registerAllyDeath()
    {
        allyKilledCount++;
        if (allyKilledText != null)
            allyKilledText.text = allyKilledCount.ToString();
    }


    public void CheckEnemyRemaining()
    {
        Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        if (enemies.Length == 0 )
        {
            ShowVictory();
        }
    }
    private void ShowVictory()
    {
        if (!VictoryPanel.activeSelf)
        {
            VictoryPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
