using UnityEngine;
using UnityEngine.UI; // Make sure to include this if you're using UI elements

public class ScoreManager : MonoBehaviour
{
    public void ResetScore() {
        score = 0;
        scoreText.text = "Score: 0";
    }
        public static ScoreManager Instance; // Singleton instance

    public Text scoreText; // Assign this from the inspector
    private int score = 0;

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: Only if you want the score to persist between scene loads
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        // Update the score text
        scoreText.text = "Score: " + score;
    }
}
