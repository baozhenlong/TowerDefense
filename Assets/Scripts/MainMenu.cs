using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public string sceneName = "Level01";
    public SceneFader sceneFader;

    public void OnClickPlay()
    {
        sceneFader.LoadScene(sceneName);
    }

    public void OnClickQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
