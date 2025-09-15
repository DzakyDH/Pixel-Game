using UnityEngine;

public class TurnManajer : MonoBehaviour
{
    public static TurnManajer Instance {get; private set;}

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    public void NextTurn()
    {
        KingdomManajer.Instance.AdvanceWeek();
        KingdomManajer.Instance.ProduceResources();
        KingdomManajer.Instance.UpdateUI();
    }
}
