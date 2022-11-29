using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool isGameOver;
    public GameObject gameOverUI;
    public GameObject winLevelUI;

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
        gameOverUI.SetActive(true);
    }

    public void WinLevel()
    {
        isGameOver = true;
        winLevelUI.SetActive(true);
    }
}
