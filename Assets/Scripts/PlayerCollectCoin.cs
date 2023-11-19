using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Namespace for TextMeshPro

public class PlayerCollectCoin : MonoBehaviour
{
    public int score = 0; // Initial score
    public TextMeshProUGUI scoreText; // Reference to the TextMeshPro GUI element
    [SerializeField] private AudioSource collectCoinAudioSource; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString(); // Update the TextMeshPro text
    }
}
