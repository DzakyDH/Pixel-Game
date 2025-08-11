using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnManajer : MonoBehaviour
{
    public TMP_Text coinText;
    public TMP_Text woodText;
    public TMP_Text stoneText;
    public TMP_Text ironText;
    public TMP_Text foodText;
    public TMP_Text citizenText;

    public int coinIncrease = 5;
    public int maxCoinLimit = 200;

    public int woodIncrease = 2;
    public int maxWoodLimit = 100;

    public int stoneIncrease = 1;
    public int maxStoneLimit = 50;

    public int ironIncrease = 1;
    public int maxIronLimit = 50;

    public int foodIncrease = 5;
    public int maxFoodLimit = 100;

    public int citizenIncrease = 1;
    public int maxCitizenLimit = 20;


    public TMP_Text weekText;
    public Button nextTurnButton;

    private KingdomManajer kingdom;

    private void Start()
    {
        kingdom = KingdomManajer.Instance;

        if (nextTurnButton != null)
            nextTurnButton.onClick.AddListener(OnNextTurn);

        UpdateUI();
    }
    private void UpdateUI()
    {
            coinText.text = $"{kingdom.coin}/{kingdom.maxCoin}";
            coinText.color = kingdom.coin >= kingdom.maxCoin ? Color.red : Color.white;

            woodText.text = $"{kingdom.wood}/{kingdom.maxWood}";
            woodText.color = kingdom.wood >= kingdom.maxWood ? Color.red : Color.white;

            stoneText.text = $"{kingdom.stone}/{kingdom.maxStone}";
            stoneText.color = kingdom.stone >= kingdom.maxStone ? Color.red : Color.white;

            ironText.text = $"{kingdom.iron}/{kingdom.maxIron}";
            ironText.color = kingdom.iron >= kingdom.maxIron ? Color.red : Color.white;

            foodText.text = $"{kingdom.food}/{kingdom.maxFood}";
            foodText.color = kingdom.food >= kingdom.maxFood? Color.red : Color.white;

            citizenText.text = $"{kingdom.citizen}/{kingdom.maxCitizen}";
            citizenText.color = kingdom.citizen >= kingdom.maxCitizen ? Color.red : Color.white;

    }
    private void OnNextTurn()
    {
        kingdom.week++;

        kingdom.coin = Mathf.Min(kingdom.coin + 5, kingdom.maxCoin);
        kingdom.wood = Mathf.Min(kingdom.wood + 2, kingdom.maxWood);
        kingdom.stone = Mathf.Min(kingdom.stone + 1, kingdom.maxStone);
        kingdom.iron = Mathf.Min(kingdom.iron + 1, kingdom.maxIron);
        kingdom.food = Mathf.Min(kingdom.food + 5, kingdom.maxFood);
        kingdom.citizen = Mathf.Min(kingdom.citizen + 1, kingdom.maxCitizen);

        UpdateUI();
    }
}
