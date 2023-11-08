// using UnityEngine;
// using UnityEngine.SceneManagement;
// using UnityEngine.UI;

// public class GameManager : MonoBehaviour
// {
//     public static GameManager Instance { get; private set; }

//     public GameObject replayButton;

//     private void Awake()
//     {
//         // Singleton pattern
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
//         // Stop the game
//         Time.timeScale = 0f;
//         // Show the replay button
//         replayButton.SetActive(true);
//     }

//     public void ReplayGame()
//     {
//         // Reset the score
//         PlayerController.Instance.score = 0;
//         UIManager.Instance.UpdateScoreText(0);
        
//         // Restart the game
//         Time.timeScale = 1f;
//         SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
//         // Alternatively, if you want to reset the scene without reloading:
//         // ResetPlayerPosition();
//         // ResetLevelState();
//         // replayButton.SetActive(false);
//     }
// }
