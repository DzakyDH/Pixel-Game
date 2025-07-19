using TMPro;
using UnityEngine;

public class DefeatManajer : MonoBehaviour
{
    public static DefeatManajer instance;

    public TMP_Text enemyKilledText;
    public TMP_Text allyKilledText;

    private int enemyKilledCount = 0;
    private int allyKilledCount = 0;

    public GameObject DefeatPanel;

    private void Awake()
    {
        instance = this;
        DefeatPanel.SetActive(false);

        if (enemyKilledText != null) enemyKilledText.text = "0";
        if (allyKilledText != null) allyKilledText.text = "0";
    }

    public void registerEnemyDeath()
    {
        enemyKilledCount++;
        if (enemyKilledText != null)
            enemyKilledText.text = enemyKilledCount.ToString();
    }

    public void registerAllyDeath()
    {
        allyKilledCount++;
        if (allyKilledText != null)
            allyKilledText.text = allyKilledCount.ToString();
        CheckAllyRemaining();
    }

    public void CheckAllyRemaining()
    {
        Ally[] allies = FindObjectsByType<Ally>(FindObjectsSortMode.None);
        Player[] player = FindObjectsByType<Player>(FindObjectsSortMode.None);
        if (allies.Length == 0 && player.Length == 0)
        {
            ShowDefeat();
        }
    }
    private void ShowDefeat()
    {
        if (!DefeatPanel.activeSelf)
        {
            DefeatPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
