using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float offsetY = 2f; // Offset for the camera's Y position

    private void Update()
    {
        if (player != null)
        {
            // Check if the player is jumping
            PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
            if (playerMovement != null && !playerMovement.IsJumping()) // Assuming IsJumping() is a method to check jump state
            {
                // Update camera position to follow the player on the X and Y axis
                transform.position = new Vector3(player.position.x, player.position.y + offsetY, transform.position.z);
            }
            else
            {
                // Follow only on the X axis when jumping
                transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
            }
        }
    }
}
