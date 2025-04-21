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


public bool CanAfford(int cost) {
    return playerCollectCoin.score >= cost;
}

public GameObject confirmationPopup;
public void ShowBuyConfirmation() {
    if (confirmationPopup != null) confirmationPopup.SetActive(true);
}

public void RefundItem(int cost) {
    playerCollectCoin.score += cost;
    playerCollectCoin.UpdateScoreText();
}

public void BuyMultipleItems(int count, int costPerItem) {
    int totalCost = count * costPerItem;
    if (CanAfford(totalCost)) {
        playerCollectCoin.score -= totalCost;
        playerCollectCoin.UpdateScoreText();
    }
}

public AudioClip buySound;
public void PlayBuySound() {
    if (buySound != null) AudioSource.PlayClipAtPoint(buySound, transform.position);
}

public float discountPercent = 0f;
public int GetDiscountedPrice(int price) {
    return Mathf.RoundToInt(price * (1f - discountPercent));
}

public GameObject itemPreviewUI;
public void PreviewItem(GameObject item) {
    if (itemPreviewUI != null) itemPreviewUI.SetActive(true);
}

public GameObject limitedTimeBanner;
public void ShowLimitedTimeOffer(bool show) {
    if (limitedTimeBanner != null) limitedTimeBanner.SetActive(show);
}

public Animator shopAnimator;
public void AnimateBuyUI() {
    if (shopAnimator != null) shopAnimator.SetTrigger("Buy");
}

private bool buyCooldown = false;
public void StartBuyCooldown(float duration) {
    buyCooldown = true;
    Invoke("EndBuyCooldown", duration);
}
private void EndBuyCooldown() {
    buyCooldown = false;
}

public GameObject tooltipUI;
public void ShowItemTooltip(string description) {
    if (tooltipUI != null) tooltipUI.GetComponent<TMP_Text>().text = description;
}

public void RandomizeDailyDeals(GameObject[] items) {
    // Shuffle items and pick a few for daily deals
}

private int loyaltyPoints = 0;
public void AddLoyaltyPoints(int amount) {
    loyaltyPoints += amount;
}

private List<string> purchaseHistory = new List<string>();
public void TrackPurchase(string itemName) {
    purchaseHistory.Add(itemName);
}

public void GiftItem(GameObject item, PlayerCollectCoin recipient) {
    recipient.ReceiveGift(item);
}

public int bulkDiscountThreshold = 5;
public float bulkDiscountPercent = 0.2f;
public int GetBulkDiscountPrice(int count, int price) {
    if (count >= bulkDiscountThreshold) return Mathf.RoundToInt(price * (1f - bulkDiscountPercent));
    return price;
}

private List<GameObject> favoriteItems = new List<GameObject>();
public void AddFavoriteItem(GameObject item) {
    if (!favoriteItems.Contains(item)) favoriteItems.Add(item);
}

public float restockTime = 3600f;
public void ShowRestockTimer(float timeLeft) {
    // Display timer in UI
}

public void HighlightNewItem(GameObject item) {
    // Add highlight effect to item in UI
}

public bool useAltCurrency = false;
public void BuyWithAltCurrency(int altAmount) {
    // Deduct alternate currency and process purchase
}

public void LockItem(GameObject item, bool locked) {
    // Set lock icon and disable buy button if locked
}

public AudioClip confirmSound;
public void PlayConfirmSound() {
    if (confirmSound != null) AudioSource.PlayClipAtPoint(confirmSound, transform.position);
}

public void PreviewItemStats(GameObject item) {
    // Show item stats in UI
}

private List<GameObject> buybackList = new List<GameObject>();
public void AddToBuyback(GameObject item) {
    buybackList.Add(item);
}
public void BuyBackItem(GameObject item) {
    if (buybackList.Contains(item)) buybackList.Remove(item);
}

