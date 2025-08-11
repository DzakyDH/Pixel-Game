using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UpgradeUIManajer : MonoBehaviour
{
    public static UpgradeUIManajer Instance;

    public DataBuilding buildingData;
    

    public TextMeshProUGUI currentLevelText;
    public TextMeshProUGUI nextLevelText;

    public TextMeshProUGUI coinCostText, woodCostText, stoneCostText, ironCostText;
   
    public TextMeshProUGUI currentProdText1, nextProdText1, currentProdText2, nextProdText2;

    public Button upgradeButton;


    private void Awake()
    {
        Instance = this;
    }
   
    public void RefreshUI()
    {
        if (buildingData == null) return;

        bool isMax = buildingData.IsMaxLevel();

        currentLevelText.text = $"LV.  {buildingData.level}";
        nextLevelText.text = isMax ? "MAX" : $"LV.{buildingData.level + 1}"; 

        coinCostText.text = isMax ? "-" : buildingData.GetCost(ResourceType.Coin).ToString();
        woodCostText.text = isMax ? "-" : buildingData.GetCost(ResourceType.Wood).ToString();
        stoneCostText.text = isMax ? "-" : buildingData.GetCost(ResourceType.Stone).ToString();
        ironCostText.text = isMax ? "-" : buildingData.GetCost(ResourceType.Iron).ToString();

        string name = (buildingData.buildingName ?? "").ToLower();

        if (name.Contains("farm") || name.Contains("mine") || name.Contains("lumber"))
        {
            currentProdText1.text = buildingData.GetCurrentProduction().ToString();
            nextProdText1.text = buildingData.IsMaxLevel() ? "-" : buildingData.productionPerlevel[buildingData.level + 1].ToString();

            currentProdText2.text = buildingData.GetCurrentMaxResource().ToString();
            nextProdText2.text = buildingData.IsMaxLevel() ? "-" : buildingData.maxResourcePerLevel[buildingData.level + 1].ToString();
        }
        else if (buildingData.buildingName.ToLower().Contains("house")) 
        {
            currentProdText1.text = buildingData.GetCurrentCoinProduction().ToString();
            nextProdText1.text = buildingData.IsMaxLevel() ? "-" : buildingData.productionPerlevel[buildingData.level + 1].ToString();

            currentProdText2.text = buildingData.maxCtizenPerLevel[buildingData.level].ToString();
            nextProdText2.text = buildingData.IsMaxLevel() ? "-" : buildingData.maxCtizenPerLevel[buildingData.level + 1].ToString();
        }
        else if (buildingData.buildingName.ToLower().Contains("barrack"))
        {
            currentProdText1.text = buildingData.GetCurrentSoldierLevel().ToString();
            nextProdText1.text = buildingData.IsMaxLevel() ? "-" : buildingData.soldierLevelPerLevel[buildingData.level + 1].ToString();

            currentProdText2.text = buildingData.GetCurrentMaxTroop().ToString();
            nextProdText2.text = buildingData.IsMaxLevel() ? "-" : buildingData.maxTroopsPerLevel[buildingData.level + 1].ToString();
        }
        else if (name.Contains("Wall"))
        {
            currentProdText1.text = buildingData.GetCurrentWallHP().ToString();
            nextProdText1.text = isMax ? "-" : SafeArrayValue(buildingData.wallHPPerLevel, buildingData.level + 1).ToString();

            currentProdText2.text = "-";
            nextProdText2.text = "-";
        }
        else
        {
            currentProdText1.text = "-";
            nextProdText1.text = "-";
            currentProdText2.text = "-";
            nextProdText2.text = "-";
        }
        bool canUpgradeCastle = buildingData.CanUpgrade();

        int coinCost = buildingData.GetCost(ResourceType.Coin);
        int woodCost = buildingData.GetCost(ResourceType.Wood);
        int stoneCost = buildingData.GetCost(ResourceType.Stone);
        int ironCost = buildingData.GetCost(ResourceType.Iron);

        bool canAfford = KingdomManajer.Instance != null &&
            KingdomManajer.Instance.CanAfford(coinCost, woodCost, stoneCost, ironCost);

        if (KingdomManajer.Instance != null && buildingData.buildingName.ToLower() != "castle")
        {
            if (buildingData.level >= KingdomManajer.Instance.GetCastleLevel())
            {
                canUpgradeCastle = false;
            }
        }
        upgradeButton.interactable = !isMax && canUpgradeCastle && canAfford;

    }
    public void RefreshAllBuildingButtons()
    {
#if UNITY_2023_1_OR_NEWER
        var all = Object.FindObjectsByType<UpgradeUIManajer>(FindObjectsSortMode.None);
#else
        var all = FindObjectsOfType<UpgradeUIManajer>
#endif
    }
    private int SafeArrayValue(int[] arr, int idx)
    {
        return (arr != null && idx >= 0 && idx < arr.Length) ? arr[idx] : 0;
    }
}
