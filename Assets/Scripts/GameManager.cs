using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void RestartGame()
    {
        Time.timeScale = 1; // Restore time
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }

    public void ExitGame()
    {
        Application.Quit(); // Exit the application
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stop play mode in the editor
        #endif
    }
}
