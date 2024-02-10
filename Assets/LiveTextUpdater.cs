using UnityEngine;
using UnityEngine.UI;

public class LiveTextUpdater : MonoBehaviour
{
    [SerializeField] private LifeManager lifeManager;
    private Text livesTextComponent;

    void Start()
    {
        livesTextComponent = gameObject.GetComponent<Text>();
        LifeManager.OnLivesUpdated += UpdateLives;
    }

    private void OnDestroy()
    {
        LifeManager.OnLivesUpdated -= UpdateLives;
    }

    private void UpdateLives(int newLives)
    {
        Debug.Log($"Lives updated {lifeManager.CurrentLives}");
        livesTextComponent.text = $"LIVES: {lifeManager.CurrentLives}";
    }
}
