using UnityEngine;
using TMPro;
using System.Collections;

public class RoundsSurvived : MonoBehaviour
{
    public TextMeshProUGUI roundsText;

    private void OnEnable()
    {
        StartCoroutine(AnimateText());
    }

    IEnumerator AnimateText()
    {
        roundsText.text = "0";
        int rounds = 0;
        yield return new WaitForSeconds(.7f);
        while (rounds < PlayerStats.rounds)
        {
            rounds += 1;
            roundsText.text = rounds.ToString();
            yield return new WaitForSeconds(.1f);
        }
    }
}
