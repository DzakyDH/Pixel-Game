using UnityEngine;

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
    public static KingdomManajer Instance;
    public int week = 1;
    public int castleLevel = 1;
    public int maxCastleLevel = 5;

    public int coinIncreasedPerLevel = 200;
    public DataBuilding castleBuilding;


    public int coin, iron, stone, wood, food, citizen;
    public int maxCoin, maxIron, maxStone, maxWood, maxFood, maxCitizen;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }


    public void AddResource(ResourceType type, int amount)
    {
        switch(type)
        {
            case ResourceType.Coin: coin = Mathf.Min(coin + amount, maxCoin); break;
            case ResourceType.Iron: iron = Mathf.Min(iron + amount, maxIron); break;
            case ResourceType.Stone: stone = Mathf.Min(stone + amount, maxStone); break;
            case ResourceType.Wood: wood = Mathf.Min(wood + amount, maxWood); break;
            case ResourceType.Food: food = Mathf.Min(food + amount, maxFood); break;
            case ResourceType.Citizen: citizen = Mathf.Min(citizen + amount, maxCitizen); break;
        }

    }
    public bool CanAfford(int coinCost, int woodCost, int stoneCost, int ironCost)
    {
        return coin >= coinCost &&
            wood >= woodCost &&
            stone >= stoneCost &&
            iron >= ironCost;
    }
    public bool SpendResources(int coinCost, int woodCost, int stoneCost, int ironCost)
    {

        return coin >= coinCost &&
               wood >= woodCost &&
               stone >= stoneCost &&
               iron >= ironCost;
            
 
    }

    public void AdvanceWeek()
    {
        week++;
    }
    public void UpgradeCastle()
    {
        if (castleLevel >= maxCastleLevel)
        {
            Debug.Log("Castle sudah di level maksimal");
            return;
        }

        castleLevel++;
        maxCoin += coinIncreasedPerLevel;

        if (UpgradeUIManajer.Instance != null)
        {
            UpgradeUIManajer.Instance.RefreshAllBuildingButtons();
        }
        else
        {
#if UNITY_2023_1_OR_NEWER
            var allUI = Object.FindObjectsByType<UpgradeUIManajer>(FindObjectsSortMode.None);
            
#else
            var ui = FindObjectOfType<UpgradeUIManajer>();
          
#endif
            foreach (var u in allUI) u.RefreshUI();
        }
    }
    public int GetCastleLevel()
    {
        return castleBuilding != null ? castleBuilding.level : castleLevel;
    }
}

