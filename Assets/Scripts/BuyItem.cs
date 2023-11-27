using System.Collections;
using UnityEngine;
using UnityEngine.UI;  // This is the line you need to include
using TMPro;

public class BuyItem : MonoBehaviour
{
    public GameObject buyUI; // Reference to the Buy UI GameObject
    public PlayerHealth playerHealth; // Reference to the PlayerHealth script
    public PlayerMovement playerMovement; // Reference to the PlayerMovement script
    public PlayerCollectCoin playerCollectCoin; // Reference to the PlayerCollectCoin script
    public GameObject buyCherryAfterText; // Reference to the BuyCherryAfterText GameObject
    public GameObject buyPowerRunAfterText; // Reference to the BuyPowerRunAfterText GameObject
    public GameObject minusCoinText; // Reference to the MinusCoinText GameObject

    

    public void ActivateBuyUI()
    {
        if (buyUI != null && buyUI.activeSelf == false)
        {
            buyUI.SetActive(true); // Activate the Buy UI
        }
    }

    public void CloseBuyUI()
    {
        if (buyUI != null)
        {
            buyUI.SetActive(false); // Deactivate the Buy UI
        }
    }

    public void BuyCherry()
    {
        Debug.Log("HIT CHERRY BUTTON");
        if (buyUI != null)
        {
            buyUI.SetActive(false); // Deactivate the Buy UI immediately
        }

        if (playerCollectCoin.score >= 3) // Check if the player has enough score
        {
            playerCollectCoin.score -= 3; // Reduce score by 3
            StartCoroutine(DelayUpdateScoreText(1f)); // Delay update score text

            ResetAndFade(minusCoinText, 2f); // Reset alpha, show, and start fade coroutine for minusCoinText

            StartCoroutine(HealPlayerAfterDelay(0f)); // Heal player immediately

            ResetAndFade(buyCherryAfterText, 2f); // Reset alpha, show, and start fade coroutine for buyCherryAfterText
        }
    }
    
    public void BuyPowerRun()
    {
        Debug.Log("HIT POWER RUN BUTTON");
        if (buyUI != null)
        {
            buyUI.SetActive(false); // Deactivate the Buy UI immediately
        }

        if (playerCollectCoin.score >= 3) // Check if the player has enough score
        {

            playerCollectCoin.score -= 3; // Reduce score by 3
            StartCoroutine(DelayUpdateScoreText(1f)); // Delay update score text

            ResetAndFade(minusCoinText, 2f); // Reset alpha, show, and start fade coroutine for minusCoinText

            playerHealth.StartPowerRun(5); // Start invulnerability for 5 seconds
            playerMovement.ModifySpeed(2f, 5f); // Double the speed for 5 seconds

            ResetAndFade(buyPowerRunAfterText, 2f); // Reset alpha, show, and start fade coroutine for buyPowerRunAfterText
        }
}

    private IEnumerator HealPlayerAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (playerHealth != null)
        {
            playerHealth.Heal(25f); // Heal the player
        }
    }

    private void ResetAndFade(GameObject textObject, float fadeDuration)
    {
        CanvasGroup canvasGroup = textObject.GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1; // Reset alpha to fully opaque
            textObject.SetActive(true); // Activate the object to make it visible
            StartCoroutine(FadeOutTextComponent(fadeDuration, textObject)); // Start the fade coroutine
        }
        else
        {
            Debug.LogError("CanvasGroup component not found on the object to fade.");
        }
    }

    private IEnumerator FadeOutTextComponent(float fadeDuration, GameObject textObject)
    {
        CanvasGroup canvasGroup = textObject.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogError("CanvasGroup component not found on the object to fade.");
            yield break;
        }
        
        // Fade from opaque to transparent.
        float startAlpha = canvasGroup.alpha;
        float rate = 1.0f / fadeDuration;
        float progress = 0.0f;

        while (progress < 1.0f)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0, progress);
            progress += rate * Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 0; // Ensure it is completely transparent
        textObject.SetActive(false); // Deactivate the object after fade out
    }

    private IEnumerator DelayUpdateScoreText(float delay)
    {
        yield return new WaitForSeconds(delay);
        playerCollectCoin.UpdateScoreText(); // Update the score display after delay
    }
}


