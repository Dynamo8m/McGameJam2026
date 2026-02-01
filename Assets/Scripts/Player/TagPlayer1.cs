using UnityEngine;
using UnityEngine.SceneManagement;

public class SpriteChangeOnCOllision : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite originalSprite;
    public Sprite alternateSprite;

    private bool isAlternateSprite = false; //Is demon?

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

    if (collision.gameObject.CompareTag("Exit") && !isAlternateSprite) //Contact with gate, detects that a "human" is touching the gate //Can only detect if demon only not using animation
    {
            Debug.Log("exit contact");
            SceneManager.LoadScene("Hell_2"); //TODO change scene corresponding to current level? (I.e. 1->2, 2->3)
    }
}

     private void ToggleSprite()
    {
        if (spriteRenderer != null)
        {
            if (isAlternateSprite)
            {
                spriteRenderer.sprite = originalSprite;
                isAlternateSprite = false; //Is human
            }
            else
            {
                spriteRenderer.sprite = alternateSprite;
                isAlternateSprite = true; //Is demon
            }
        }
    }

    private void Update()
    {
        Debug.Log(isAlternateSprite);
        if (spriteRenderer.sprite == alternateSprite)
        {
            isAlternateSprite = true;
        }
        else
        {
            isAlternateSprite = false;
        }
    }
}