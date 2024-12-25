using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public GameObject finishUI; // Reference to the Finish UI element

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Time.timeScale = 0; // Stop the game time
            if (finishUI != null)
            {
                finishUI.SetActive(true); // Enable the Finish UI element
            }
        }
    }
}
