using UnityEngine;

public class Block : MonoBehaviour
{
    public delegate void OnBlockDestroyedType(float resiliencePoints);
    public static event OnBlockDestroyedType OnBlockDestroyed;
    public delegate void OnBlockHitType();
    public static event OnBlockHitType OnBlockHit;
    [SerializeField] private float maxHitPoints = 1;

    float currentHealthPoints;

    private void Start()
    {
        currentHealthPoints = maxHitPoints;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            currentHealthPoints -= 1;
            if (currentHealthPoints == 0)
            {
                Destroy(gameObject);
                Debug.Log("Block Destroyed!");
                OnBlockDestroyed?.Invoke(maxHitPoints);
            }
            else
            {
                Debug.Log("Block Hit!");
                OnBlockHit?.Invoke();
            }
        }
    }
}
