using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Namespace for TextMeshPro
using UnityEngine.UI; // Namespace for UI


public class PlayerCollectCoin : MonoBehaviour
{
    public void MultiplyCoins(int multiplier) {
        score *= multiplier;
        UpdateScoreText();
    }
        private int totalCoinsCollected = 0;
    public void TrackCoinStats(int amount) {
        totalCoinsCollected += amount;
    }
        private bool coinLock = false;
    public void SetCoinLock(bool enable) {
        coinLock = enable;
    }
        public void ShowCoinGainPopup(int amount) {
        // Instantiate popup at player position
    }
        public AudioClip streakSound;
    public void PlayStreakSound() {
        if (streakSound != null) collectCoinAudioSource.PlayOneShot(streakSound);
    }
        private int comboCounter = 0;
    public void AddCombo() {
        comboCounter++;
        if (comboCounter > 10) Debug.Log("Big combo!");
    }
        public void SetCoinCount(int value) {
        score = value;
        UpdateScoreText();
    }
        public void UnlockCoinAchievements() {
        if (score >= 100) Debug.Log("Achievement: 100 Coins!");
    }
        public void RefundCoins(int amount) {
        score += amount;
        UpdateScoreText();
    }
        public float rareCoinChance = 0.01f;
    public void TrySpawnRareCoin() {
        if (Random.value < rareCoinChance) Debug.Log("Rare coin spawned!");
    }
        public delegate void CoinCountChanged(int newCount);
    public event CoinCountChanged OnCoinCountChanged;
    private void NotifyCoinCountChanged() {
        if (OnCoinCountChanged != null) OnCoinCountChanged(score);
    }
        public void AnimateCoinIcon() {
        if (scoreText != null) scoreText.CrossFadeAlpha(0.5f, 0.2f, false);
    }
        public bool CanAfford(int amount) {
        return score >= amount;
    }
        public void DecayCoinsOverTime(float rate) {
        score = Mathf.Max(0, score - Mathf.RoundToInt(rate * Time.deltaTime));
        UpdateScoreText();
    }
        public void SubmitCoinsToLeaderboard() {
        Debug.Log("Coins submitted: " + score);
    }
        public bool doubleCoinsActive = false;
    public void ToggleDoubleCoins(bool enable) {
        doubleCoinsActive = enable;
        coinMultiplier = enable ? 2 : 1;
    }
        public void SaveCoinCount() {
        PlayerPrefs.SetInt("coins", score);
        PlayerPrefs.Save();
    }
    public void LoadCoinCount() {
        score = PlayerPrefs.GetInt("coins", 0);
        UpdateScoreText();
    }
        public void DropCoinsOnDamage(int amount) {
        score = Mathf.Max(0, score - amount);
        UpdateScoreText();
    }
        public void AddDebt(int amount) {
        score -= amount;
        UpdateScoreText();
    }
        public void AnimateCoinHUD() {
        if (scoreText != null) scoreText.transform.localScale = Vector3.one * 1.2f;
    }
        public void SpendCoins(int amount) {
        if (score >= amount) {
            score -= amount;
            UpdateScoreText();
        }
    }
        private int coinStreak = 0;
    public void AddCoinStreak() {
        coinStreak++;
        if (coinStreak % 5 == 0) SetCoinMultiplier(coinMultiplier + 1);
    }
    public void ResetCoinStreak() {
        coinStreak = 0;
        SetCoinMultiplier(1);
    }
        public delegate void CoinMilestone(int coins);
    public event CoinMilestone OnCoinMilestone;
    public void CheckCoinMilestone(int milestone) {
        if (score >= milestone && OnCoinMilestone != null) OnCoinMilestone(score);
    }
        public AudioClip goldenCoinSound;
    public void PlayGoldenCoinSound() {
        if (goldenCoinSound != null) collectCoinAudioSource.PlayOneShot(goldenCoinSound);
    }
        public GameObject floatingTextPrefab;
    public void ShowFloatingText(string text) {
        if (floatingTextPrefab != null) Instantiate(floatingTextPrefab, transform.position, Quaternion.identity).GetComponent<TMPro.TextMeshProUGUI>().text = text;
    }
        public void ResetCoinScore() {
        score = 0;
        UpdateScoreText();
    }
        public float magnetRadius = 3f;
    public void ActivateCoinMagnet() {
        Collider2D[] coins = Physics2D.OverlapCircleAll(transform.position, magnetRadius);
        foreach (var c in coins) {
            if (c.CompareTag("Coin")) c.transform.position = Vector3.MoveTowards(c.transform.position, transform.position, 0.2f);
        }
    }
        public int coinMultiplier = 1;
    public void SetCoinMultiplier(int multiplier) {
        coinMultiplier = multiplier;
    }
        public int score = 0; // Initial score
    public TextMeshProUGUI scoreText; // Reference to the TextMeshPro GUI element
    [SerializeField] private AudioSource collectCoinAudioSource; 
    public Button buyItemButton; // Reference to the BuyItem button
    public Animator buyButtonAnimator; // Reference to the Animator component on the BuyItemButton


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

    void Update()
    {
        // Check the score each frame and update the button's interactable state
        if (score >= 3)
        {
            buyItemButton.interactable = true; // Enable the BuyItem button
        }
        else
        {
            buyItemButton.interactable = false; // Disable the BuyItem button
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        Debug.Log("Collision Detected with: " + collision.gameObject.name);
        // Check if the collided object is a coin
        if (collision.gameObject.CompareTag("Coin"))
        {
            collectCoinAudioSource.Play();
            Debug.Log("Collided with a coin!");
            collision.gameObject.GetComponent<Collider2D>().enabled = false; // Disable the collider
            Destroy(collision.gameObject); // Destroy the coin
            score++; // Increase the score
            UpdateScoreText(); // Update the score display
        }

    }

    public void UpdateScoreText()
    {
        scoreText.text = "Coins: " + score.ToString(); // Update the TextMeshPro text
        // if (score >= 3)
        // {
        //     buyItemButton.interactable = true; // Enable the BuyItem button
        // }
    }
}
