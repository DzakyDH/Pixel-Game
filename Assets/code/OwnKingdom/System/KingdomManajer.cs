using UnityEngine;
using TMPro;
public enum ResourceType
{
    Coin,
    Iron,
    Stone,
    Wood,
    Food,
    Citizen
}
public class KingdomManajer : MonoBehaviour
{
    public static KingdomManajer Instance { get; private set; }

    public int coin, iron, stone, wood, food, citizen;
    public int maxCoin, maxIron, maxStone, maxWood, maxFood, maxCitizen;
    public int coinProduction, ironProduction, stoneProduction, woodProduction, foodProduction, citizenProduction;
    public int week = 1;

    public TMP_Text coinText, woodText, stoneText, ironText, foodText, citizenText, weekText;
    private void Awake()
    {
        if (Instance == null) Instance = this;

        else Destroy(gameObject);

    }
    private void Start()
    {
        UpdateUI();
    }
    public void AdvanceWeek()
    {
        week++;
    }
    public void ProduceResources()
    {
        AddResource(ResourceType.Coin, coinProduction);
        AddResource(ResourceType.Wood, woodProduction);
        AddResource(ResourceType.Stone, stoneProduction);
        AddResource(ResourceType.Iron, ironProduction);
        AddResource(ResourceType.Food, foodProduction);
        AddResource(ResourceType.Citizen, citizenProduction);
    }
    public void AddResource(ResourceType type, int amount)
    {
        switch (type)
        {
            case ResourceType.Coin: coin = Mathf.Min(coin + amount, maxCoin); break;
            case ResourceType.Iron: iron = Mathf.Min(iron + amount, maxIron); break;
            case ResourceType.Stone: stone = Mathf.Min(stone + amount, maxStone); break;
            case ResourceType.Wood: wood = Mathf.Min(wood + amount, maxWood); break;
            case ResourceType.Food: food = Mathf.Min(food + amount, maxFood); break;
            case ResourceType.Citizen: citizen = Mathf.Min(citizen + amount, maxCitizen); break;
        }
        UpdateUI();
    }
    public bool HasEnoughResource(int coinCost, int woodCost, int stoneCost, int ironCost)
    {
        return coin >= coinCost &&
            wood >= woodCost &&
            stone >= stoneCost &&
            iron >= ironCost;
    }
    public void SpendResources(int coinCost, int woodCost, int stoneCost, int ironCost)
    {
        coin -= coinCost;
        wood -= woodCost;
        stone -= stoneCost;
        iron -= ironCost;

        UpdateUI();
    }

    public void UpdateUI()
    {
        coinText.text = $"{coin}/{maxCoin}";
        woodText.text = $"{wood}/{maxWood}";
        stoneText.text = $"{stone}/{maxStone}";
        ironText.text = $"{iron}/{maxStone}";
        foodText.text = $"{food}/{maxStone}";
        citizenText.text = $"{citizen}/{maxCitizen}";
        weekText.text = $"Week {week}";

        SetColor(coinText, coin >= maxCoin);
        SetColor(woodText, wood >= maxWood);
        SetColor(stoneText, stone >= maxStone);
        SetColor(ironText, iron >= maxIron);
        SetColor(foodText, food >= maxFood);
        SetColor(citizenText, citizen >= maxCitizen);
    }
    private void SetColor(TMP_Text text, bool isFull)
    {
        text.color = isFull ? Color.red : Color.white;
    }
}