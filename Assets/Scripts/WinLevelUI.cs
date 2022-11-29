using UnityEngine;

public class WinLevelUI : MonoBehaviour
{
    public SceneFader sceneFader;
    public string mainMenuSceneName = "MainMenu";

    public void OnClickContinue()
    {
        PlayerPrefs.SetInt("levelReached", 2);
        sceneFader.LoadScene("Level02");
    }

    public void OnClickMainMenu()
    {
        sceneFader.LoadScene(mainMenuSceneName);
    }
}
