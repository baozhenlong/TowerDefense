using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public string mainMenuSceneName = "MainMenu";
    public SceneFader sceneFader;

    public void OnClickRetry()
    {
        sceneFader.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickMainMenu()
    {
        sceneFader.LoadScene(mainMenuSceneName);
    }
}
