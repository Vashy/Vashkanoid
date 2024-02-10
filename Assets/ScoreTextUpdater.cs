using UnityEngine;
using UnityEngine.UI;

public class ScoreTextUpdater : MonoBehaviour
{
    [SerializeField] private Text currentScoreText;
    private int currentScore;

    void Start()
    {
        currentScoreText = gameObject.GetComponent<Text>();
        ScoreManager.OnScoreUpdated += UpdateScore;
        UpdateScore(currentScore);
    }

    private void OnDestroy()
    {
        ScoreManager.OnScoreUpdated -= UpdateScore;
    }

    private void UpdateScore(int newScore)
    {
        Debug.Log($"Score updated {newScore}");

        currentScore = newScore;
        currentScoreText.text = $"SCORE: {currentScore}";
    }
}
