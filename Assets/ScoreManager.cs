using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int CurrentScore { get; private set; } = 0;
    public delegate void OnScoreUpdatedType(int newScore);
    public static event OnScoreUpdatedType OnScoreUpdated;

    [SerializeField] private int scorePerLife = 10;

    private void Start()
    {
        Block.OnBlockDestroyed += AddScore;
    }

    private void OnDestroy()
    {
        Block.OnBlockDestroyed -= AddScore;
    }

    private void AddScore(float scoreToAdd)
    {
        CurrentScore += (int)scoreToAdd * scorePerLife;
        OnScoreUpdated?.Invoke(CurrentScore);
    }
}
