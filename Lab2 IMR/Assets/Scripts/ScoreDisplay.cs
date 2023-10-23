using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    void Update()
    {
        UpdateScoreDisplay();
    }

    public void UpdateScoreDisplay()
    {
        if (KeepScore.Score > 300)
        {
            scoreText.color = Color.green;
        }
        else if (KeepScore.Score > 100)
        {
            scoreText.color = Color.yellow;
        }
        else
        {
            scoreText.color = Color.red;
        }
        scoreText.text = "Score: " + KeepScore.Score + "\nBest Score: " + KeepScore.bestScore;
    }
}
