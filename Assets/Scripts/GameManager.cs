using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool isGameOver;

    private void Start()
    {
        isGameOver = false;
    }

    private void Update()
    {
        if (isGameOver)
        {
            return;
        }
        if (PlayerStats.Lives <= 0)
        {
            Lose();
        }
    }

    private void Lose()
    {
        isGameOver = true;
        Debug.Log("游戏结束-lose");
    }

    public void WinLevel()
    {
        isGameOver = true;
        Debug.Log("游戏结束-win");
    }
}
