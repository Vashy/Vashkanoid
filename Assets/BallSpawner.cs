using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] Ball ball;
    [SerializeField] float spawnDelay = 2f;
    bool isGameEnded = false;

    private void Start()
    {
        SpawnBallAfterDelay();
        DeadTrigger.OnDeadTriggered += SpawnBallAfterDelay;
        LifeManager.OnEndGame += StopSpawning;
        LevelManager.OnLevelFinished += StopSpawning;
    }

    private void OnDestroy()
    {
        DeadTrigger.OnDeadTriggered -= SpawnBallAfterDelay;
        LifeManager.OnEndGame -= StopSpawning;
        LevelManager.OnLevelFinished -= StopSpawning;
    }

    private void StopSpawning()
    {
        isGameEnded = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnBall();
            Debug.Log("Ball Spawned");
        }
    }

    void SpawnBall()
    {
        if (!isGameEnded)
        {
            Instantiate(ball, new Vector3(0f, -1.35f, 0f), transform.rotation);
        }
    }

    void SpawnBallAfterDelay()
    {
        Invoke(nameof(SpawnBall), spawnDelay);
    }
}
