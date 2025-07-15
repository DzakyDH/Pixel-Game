using UnityEngine;
using UnityEngine.UI;

public class BattleSetupManajer : MonoBehaviour
{
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

        Instantiate(PlayerPrefab, PlayerSpawnPoint.position, Quaternion.identity);

        for (int i = 0; i < allyCount; i++)
        {
            Vector3 offset = new Vector3 (i * 1.5f, 0, 0);
            Instantiate(allyPrefab, allySpawnPoint.position + offset, Quaternion.identity);

        }

    }

}
