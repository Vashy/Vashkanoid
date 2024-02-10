using UnityEngine;

public class DeadTrigger : MonoBehaviour
{
    public delegate void OnDeadTriggerEnteredType();
    public static event OnDeadTriggerEnteredType OnDeadTriggered;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            OnDeadTriggered?.Invoke();
            Debug.Log("Dead!");
        }
    }
}
