using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject ui;
    public string mainMenuSceneName = "MainMenu";

    public SceneFader sceneFader;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Toggle();
        }
    }

    private void Toggle()
    {
        ui.SetActive(!ui.activeSelf);
        Time.timeScale = ui.activeSelf ? 0f : 1f;
    }

    public void OnClickContinue()
    {
        Toggle();
    }

    public void OnClickRetry()
    {
        Toggle();
        sceneFader.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickMainMenu()
    {
        Toggle();
        sceneFader.LoadScene(mainMenuSceneName);
    }
}
