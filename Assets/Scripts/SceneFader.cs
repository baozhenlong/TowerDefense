using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    public Image img;
    public AnimationCurve animationCurve;

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(FadeOut(sceneName));
    }

    IEnumerator FadeIn()
    {
        float time = 1f;
        while (time > 0f)
        {
            time -= Time.deltaTime;
            img.color = new Color(0f, 0f, 0f, animationCurve.Evaluate(time));
            yield return null;
        }
    }

    IEnumerator FadeOut(string sceneName)
    {
        float time = 0f;
        while (time < 1f)
        {
            time += Time.deltaTime;
            img.color = new Color(0f, 0f, 0f, animationCurve.Evaluate(time));
            yield return null;
        }
        SceneManager.LoadScene(sceneName);
    }
}
