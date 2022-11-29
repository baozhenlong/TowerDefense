using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Currency { get; set; }
    public int startCurrency = 400;
    public static int Lives { get; set; }
    public int startLives = 20;
    public static int rounds = 0;

    private void Start()
    {
        Currency = startCurrency;
        Lives = startLives;
        rounds = 0;
    }
}
