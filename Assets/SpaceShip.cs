using UnityEngine;

public class SpaceShip : MonoBehaviour
{
    public delegate void OnShipHitType();
    public static event OnShipHitType OnShipHit;

    [SerializeField] float moveSpeed;
    [SerializeField] float maxLeftPosition = -4.2f;
    [SerializeField] float maxRightPosition = 4.2f;
    [SerializeField] float spawnDelay = 2f;

    bool isGameEnded = false;

    private void Start()
    {
        LifeManager.OnEndGame += StopSpawning;
        DeadTrigger.OnDeadTriggered += DisableSpaceShip;
        DeadTrigger.OnDeadTriggered += EnableSpaceShipWithDelay;
        LevelManager.OnLevelFinished += DisableSpaceShip;
    }

    private void OnDestroy()
    {
        LifeManager.OnEndGame -= StopSpawning;
        DeadTrigger.OnDeadTriggered -= DisableSpaceShip;
        DeadTrigger.OnDeadTriggered -= EnableSpaceShipWithDelay;
        LevelManager.OnLevelFinished -= DisableSpaceShip;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            if (transform.position.x > maxLeftPosition)
            {
                Move(Vector3.left);
            }
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            if (transform.position.x < maxRightPosition)
            {
                Move(Vector3.right);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Debug.Log("Ship Hit!");
            OnShipHit?.Invoke();
        }
    }

    private void Move(Vector3 direction)
    {
        transform.position = transform.position + (direction * moveSpeed) * Time.deltaTime;
    }

    private void DisableSpaceShip()
    {
        gameObject.SetActive(false);
    }
    
    private void EnableSpaceShip()
    {
        if (!isGameEnded)
        {
            transform.position = new Vector3(0f, transform.position.y);
            gameObject.SetActive(true);
        }
    }

    private void EnableSpaceShipWithDelay()
    {
        Invoke(nameof(EnableSpaceShip), spawnDelay);
    }

    private void StopSpawning()
    {
        isGameEnded = true;
    }
}
