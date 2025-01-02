using UnityEngine;
using UnityEngine.UI; // Make sure to include this if you're using UI elements

public class ScoreManager : MonoBehaviour
{
    public GameObject floatingScorePrefab;
    public void ShowFloatingScore(int amount, Vector3 position) {
        if (floatingScorePrefab != null) {
            GameObject popup = Instantiate(floatingScorePrefab, position, Quaternion.identity);
            popup.GetComponentInChildren<UnityEngine.UI.Text>().text = "+" + amount;
        }
    }
        public void SubtractScore(int amount) {
        score = Mathf.Max(0, score - amount);
        scoreText.text = "Score: " + score;
    }
        public void SaveScore() {
        PlayerPrefs.SetInt("score", score);
        PlayerPrefs.Save();
    }
    public void LoadScore() {
        score = PlayerPrefs.GetInt("score", 0);
        scoreText.text = "Score: " + score;
    }
        public delegate void ScoreChanged(int newScore);
    public event ScoreChanged OnScoreChanged;
    private void NotifyScoreChanged() {
        if (OnScoreChanged != null) OnScoreChanged(score);
    }
        public void ApplyMissedCollectiblePenalty(int penalty) {
        score = Mathf.Max(0, score - penalty);
        scoreText.text = "Score: " + score;
    }
        private int scoreMultiplier = 1;
    public void SetScoreMultiplier(int multiplier) {
        scoreMultiplier = multiplier;
    }
    public void AddScoreWithMultiplier(int amount) {
        AddScore(amount * scoreMultiplier);
    }
        private int highScore = 0;
    public void UpdateHighScore() {
        if (score > highScore) {
            highScore = score;
        }
        scoreText.text = "High Score: " + highScore;
    }
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
