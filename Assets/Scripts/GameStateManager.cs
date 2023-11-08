// using UnityEngine;
// using UnityEngine.SceneManagement;
// using TMPro;

// public class GameStateManager : MonoBehaviour
// {
//     public static GameStateManager Instance { get; private set; }

//     public GameObject replayButton;
//     public PlayerController playerController;

//     private void Awake()
//     {
//         if (Instance == null)
//         {
//             Instance = this;
//             DontDestroyOnLoad(gameObject);
//         }
//         else
//         {
//             Destroy(gameObject);
//         }
//     }

//     public void PlayerDied()
//     {
//         // Stop the game (if not using a physics update, otherwise set a flag and check in Update)
//         Time.timeScale = 0f;
        
//         // Show the replay button
//         replayButton.SetActive(true);
//     }

//     public void ReplayGame()
//     {
//         // Reset the score
//         playerController.ResetScore();
        
//         // Restart the time
//         Time.timeScale = 1f;
        
//         // Reload the current scene
//         SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
//         // Hide the replay button
//         replayButton.SetActive(false);
//     }
// }
