using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RetreatManajer : MonoBehaviour
{
    public static RetreatManajer Instance;

    public GameObject retreatPanel;
    public Text enemiesKilledText;
    public Text alliesLostText;

    private int enemiesKilled = 0;
    private int alliesLost = 0;

    private bool isretreating = false;
    public float retreatSpeed = 3f;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    public void AddEnemyKilled() => enemiesKilled++;
    public void AddAllyLost() => alliesLost++;

    public void TriggerRetreat()
    {
        isretreating = true;
        ShowRetreatPanel();

        Characterbase[] allUnits = Object.FindObjectsByType<Characterbase>(FindObjectsSortMode.None);
        foreach (Characterbase unit in allUnits)
        {
            if (unit.CompareTag("Ally") || unit.CompareTag("Player"))
            {
                unit.enabled = false;
                if (unit.TryGetComponent<Rigidbody2D>(out var rb))
                {
                    rb.linearVelocity = Vector2.left * retreatSpeed;
                }
            }
        }
    }
    private void ShowRetreatPanel()
    {
        if (retreatPanel != null)
        {
            retreatPanel.SetActive(true);
            UpdateStatsUi();
        }
    }
    private void UpdateStatsUi()
    {
        enemiesKilledText.text = "Enemy Killed : " + enemiesKilled;
        alliesLostText.text = "Ally Lost : " + alliesLost;
    }
    public void ExitToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}