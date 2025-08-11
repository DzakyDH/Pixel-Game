using UnityEngine;

[System.Serializable]
public class DataBuilding
{
    public string buildingName;
    public int level = 0;
    public int maxLevel = 10;

    public int[] coinCosts;
    public int[] woodCosts;
    public int[] stoneCosts;
    public int[] ironCosts;

    public int[] productionPerlevel;
    public int[] maxResourcePerLevel;

    public int[] maxCtizenPerLevel;
    public int[] soldierLevelPerLevel;
    public int[] maxTroopsPerLevel;

    public int[] wallHPPerLevel;
    public bool CanUpgrade()
    {
        if (IsMaxLevel()) return false;

        int castleLevel = KingdomManajer.Instance.castleLevel;
        return level < maxLevel && level < castleLevel;
    }
    public bool IsMaxLevel()
    {
        return level >= maxLevel;
    }

    public int GetCost(ResourceType type)
    {
        if (IsMaxLevel()) return 0;

        return type switch
        {
            ResourceType.Coin => coinCosts.Length > level ? coinCosts[level] : 0,
            ResourceType.Wood => woodCosts.Length > level ? woodCosts[level] : 0,
            ResourceType.Stone => stoneCosts.Length > level ? stoneCosts[level] : 0,
            ResourceType.Iron => ironCosts.Length > level ? ironCosts[level] : 0,
            _ => 0,
        };
    }

    public void Upgrade()
    {
        if (!IsMaxLevel())
            level++;
    }
    #region Getter Data
    public int GetCurrentProduction() => GetValue(productionPerlevel);
    public int GetCurrentMaxResource() => GetValue(maxResourcePerLevel);
    public int GetCurrentCoinProduction() => GetCurrentProduction();
    public int GetCurrentWallHP() => GetValue(wallHPPerLevel);
    public int GetCurrentSoldierLevel() => GetValue(soldierLevelPerLevel);
    public int GetCurrentMaxTroop() => GetValue(maxTroopsPerLevel);
    public int GetCurrentMaxCitizen() => GetValue(maxCtizenPerLevel);
    #endregion
    private int GetValue(int[] arr)
    {
        return (arr != null && arr.Length > level) ? arr[level] : 0;
    }
    public bool CanUprade(int castleLevel)
    {
        return level < maxLevel && level < castleLevel;
    }
}
