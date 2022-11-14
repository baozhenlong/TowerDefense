using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Currency { get; set; }
    public int startCurrency = 400;

    private void Start()
    {
        Currency = startCurrency;
    }
}
