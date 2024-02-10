using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    Vector3 direction = Vector3.down;
    const int playerSpaceshipLayer = 3;

    private void Start()
    {
        DeadTrigger.OnDeadTriggered += DestroyBall;
        LevelManager.OnLevelFinished += DestroyBall;
    }

    private void OnDestroy()
    {
        DeadTrigger.OnDeadTriggered -= DestroyBall;
        LevelManager.OnLevelFinished -= DestroyBall;
    }

    void Update()
    {
        Move(direction);
    }

    private void Move(Vector3 direction)
    {
        transform.position = transform.position + (direction * moveSpeed) * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == playerSpaceshipLayer)
        {
            Vector3 spaceShipPosition = collision.gameObject.transform.position;
            Vector3 ballPosition = transform.position;
            float spaceShipHorizontalSize = collision.gameObject.GetComponent<BoxCollider2D>().size.x;
            float relativePosition = CalculateRelativePositionBetween(ballPosition, spaceShipPosition, spaceShipHorizontalSize);
            direction = ConvertToTopArcTrigonometricVector(relativePosition).normalized;
        }
        else
        {
            ContactPoint2D[] contacts = collision.contacts;

            direction = UpdatedDirectionOnGenericCollision(contacts);
        }
    }

    private Vector3 UpdatedDirectionOnGenericCollision(ContactPoint2D[] contacts)
    {
        foreach (ContactPoint2D contact in contacts)
        {
            Vector2 normal = contact.normal;

            if (IsHorizontalCollision(normal))
            {
                // Horizontal collision
                return new Vector3(-direction.x, direction.y).normalized;
            }
            else
            {
                // Vertical collision
                return new Vector3(direction.x, -direction.y).normalized;
            }
        }
        return direction;
    }

    private static bool IsHorizontalCollision(Vector2 normal)
    {
        return Mathf.Abs(normal.x) > Mathf.Abs(normal.y);
    }

    /// <summary>
    /// Calculates the relative position between the two input vectors
    /// </summary>
    /// <returns>value in the range [-1, 1]</returns>
    private float CalculateRelativePositionBetween(Vector3 position, Vector3 relativeTo, float maxSize)
    {
        var relativePosition = (position.x - relativeTo.x) * 2 / maxSize;
        return Mathf.Clamp(relativePosition, -0.99f, 0.99f);
    }

    // Function to convert a value in the range [-1, 1] to a Vector3
    private Vector3 ConvertToTopArcTrigonometricVector(float inputValue)
    {
        // Calculate the angle in radians based on the clamped value
        float angle = Mathf.Acos(inputValue);

        // Compute the x and y components using trigonometric functions
        float x = Mathf.Cos(angle);
        float y = Mathf.Sin(angle);

        return new Vector3(x, y, 0f);
    }

    private void DestroyBall()
    {
        Destroy(gameObject);
    }
}
