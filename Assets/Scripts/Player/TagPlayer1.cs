using UnityEngine;

public class SpriteChangeOnCOllision : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite originalSprite;
    public Sprite alternateSprite;

    private bool isAlternateSprite = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalSprite = spriteRenderer.sprite;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("Player") &&
        collision.gameObject != gameObject)
    {
        ToggleSprite();
    }
}

     private void ToggleSprite()
    {
        if (spriteRenderer != null)
        {
            if (isAlternateSprite)
            {
                spriteRenderer.sprite = originalSprite;
                isAlternateSprite = false;
            }
            else
            {
                spriteRenderer.sprite = alternateSprite;
                isAlternateSprite = true;
            }
        }
    }
}