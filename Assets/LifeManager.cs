using UnityEngine;
using UnityEngine.SceneManagement;

public class LifeManager : MonoBehaviour
{
    public int CurrentLives { get { return currentLives; } }
    public delegate void OnEndGameType();
    public static event OnEndGameType OnEndGame;
    public delegate void OnLivesUpdatedType(int newLives);
    public static event OnLivesUpdatedType OnLivesUpdated;

    [SerializeField] private GameObject gameOverPrompt;
    [SerializeField] private int currentLives = 3;

    private void Start()
    {
        DeadTrigger.OnDeadTriggered += LifeLost;
    }

    private void OnDestroy()
    {
        DeadTrigger.OnDeadTriggered -= LifeLost;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void LifeLost()
    {
        currentLives--;
        OnLivesUpdated?.Invoke(currentLives);
        if (currentLives <= 0)
        {
            OnEndGame?.Invoke();
            Debug.Log("Game ended!");
            gameOverPrompt.SetActive(true);
        }
    }
}
