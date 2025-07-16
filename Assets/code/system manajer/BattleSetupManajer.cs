using UnityEngine;
using UnityEngine.UI;

public class BattleSetupManajer : MonoBehaviour
{
    public static BattleSetupManajer instance;

    public GameObject SetupPanel;
    public Slider allySlider;
    public Slider enemySlider;

    public GameObject PlayerPrefab;
    public GameObject allyPrefab;
    public GameObject enemyPrefab;

    public Transform allySpawnPoint;
    public Transform enemySpawnPoint;
    public Transform PlayerSpawnPoint;

    public Text allyCountText;
    public Text enemyCountText;

    public Player currentPlayer;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        allyCountText.text = allySlider.value.ToString();
        enemyCountText.text = enemySlider.value.ToString();
    }
    public void StartBattle()
    {
        int allyCount = (int)allySlider.value;
        int enemyCount = (int)enemySlider.value;

        SetupPanel.SetActive(false);

        GameObject playerObj = Instantiate(PlayerPrefab, PlayerSpawnPoint.position, Quaternion.identity);
        currentPlayer = playerObj.GetComponent<Player>();
        for (int i = 0; i < allyCount; i++)
        {
            Vector3 offset = new Vector3 (i * 1.5f, 0, 0);
            Instantiate(allyPrefab, allySpawnPoint.position + offset, Quaternion.identity);

        }
        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 offset = new Vector3(i * 1.5f, 0, 0);
            Instantiate(enemyPrefab, enemySpawnPoint.position + offset, Quaternion.identity);
        }
    }
    public void PressLeftDown() => currentPlayer?.StartMoveLeft();
    public void PressLeftUp() => currentPlayer?.StopMoveLeft();
    public void PressRightDown() => currentPlayer?.StartMoveRight();
    public void PressRightUp() => currentPlayer?.StopMoveRight();
    public void PressAttack() => currentPlayer?.PressAttack();
}
