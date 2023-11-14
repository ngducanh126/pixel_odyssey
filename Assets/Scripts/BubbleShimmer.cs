using System.Collections;
using UnityEngine;

public class BubbleShimmer : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public float shimmerDuration = 0.3f; // Duration of one shimmer cycle
    public float minAlpha = 0.1f; // Minimum transparency
    public float maxAlpha = 1.0f; // Maximum transparency

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(ShimmerEffect());
    }

    private IEnumerator ShimmerEffect()
    {
        while (true)
        {
            // Fade out
            float timer = 0;
            while (timer < shimmerDuration / 2)
            {
                timer += Time.deltaTime;
                float alpha = Mathf.Lerp(maxAlpha, minAlpha, timer / (shimmerDuration / 2));
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha);
                yield return null;
            }

            // Fade in
            timer = 0;
            while (timer < shimmerDuration / 2)
            {
                timer += Time.deltaTime;
                float alpha = Mathf.Lerp(minAlpha, maxAlpha, timer / (shimmerDuration / 2));
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha);
                yield return null;
            }
        }
    }
}
