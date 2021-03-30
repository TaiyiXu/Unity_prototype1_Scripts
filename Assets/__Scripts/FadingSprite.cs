using System.Collections;
using UnityEngine;


/// <summary>
/// Marks a sprite that should fade away when the player character enters it's trigger.
/// </summary>
/// <typeparam name="FadingSprite"></typeparam>
[RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
public class FadingSprite : MonoBehaviour
{
    internal SpriteRenderer spriteRenderer;

    private float alpha = 1, velocity, targetAlpha = 1;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        alpha = Mathf.SmoothDamp(alpha, targetAlpha, ref velocity, 0.1f, 1f);
        spriteRenderer.color = new Color(1, 1, 1, alpha);
        //Debug.Log(spriteRenderer.color);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        targetAlpha = 0.5f;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        targetAlpha = 1f;
    }
}
