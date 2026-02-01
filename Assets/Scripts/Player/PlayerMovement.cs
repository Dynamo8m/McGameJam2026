using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;



public class PlayerMovement : MonoBehaviour
{


   

    [SerializeField] private GameObject hitEffectPrefab;  // drag your effect prefab here
    [SerializeField] private float hitStopDuration = 0.15f; // how long to pause gameplay
    [SerializeField] private float hitEffectScale = 6f;      // "very large"
    [SerializeField] private Vector3 hitEffectOffset = new Vector3(0f, 0f, 0f);

    
    Animator animator;
    public float speed = 5f;
    private bool canJump;

    [SerializeField] private float hitInvincibilityTime = 1f;

    [SerializeField] private float hitCooldown = 1f;

    [SerializeField] private bool isControlledCharacter = true;
    private float nextAllowedHitTime = 0f;

    private bool canBeHit = true;   

    private bool nextHitIsB = false;
    private Vector3 originalScale;

    // ?? ADD THESE HERE
    public float jumpForce = 12f;
    private bool jumpPressed;

    private Rigidbody2D body;
    private Vector2 movementInput;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
    }

    private void PlayHitEffect()
    {
        if (hitEffectPrefab == null) return;

        Vector3 center = Camera.main != null ? Camera.main.transform.position : Vector3.zero;
        center.z = 0f;

        GameObject fx = Instantiate(hitEffectPrefab, center + hitEffectOffset, Quaternion.identity);
        fx.transform.localScale = Vector3.one * hitEffectScale;

        // Ensure it renders on top
        SpriteRenderer sr = fx.GetComponent<SpriteRenderer>();
        if (sr != null) sr.sortingOrder = 1000;

        // Start the FX animation from the beginning
        Animator fxAnim = fx.GetComponent<Animator>();
        if (fxAnim != null) fxAnim.Play(0, 0, 0f);

        // Pause gameplay (FX will still play because Animator is Unscaled Time)
        StartCoroutine(HitStopNextFrame());

        Destroy(fx, 2f);
    }




    private IEnumerator HitStopNextFrame()
{
    yield return null; // wait one frame so the FX gets a chance to start
    float old = Time.timeScale;
    Time.timeScale = 0f;
    yield return new WaitForSecondsRealtime(hitStopDuration);
    Time.timeScale = old;
}


    private IEnumerator GameplayPause(float duration)
    {
        // Disable movement / physics
        Rigidbody2D[] bodies = FindObjectsOfType<Rigidbody2D>();
        foreach (var rb in bodies)
            rb.simulated = false;

        yield return new WaitForSeconds(duration);

        foreach (var rb in bodies)
            rb.simulated = true;
    }


    private IEnumerator HitInvincibility()
    {
        canBeHit = false;
        yield return new WaitForSeconds(hitInvincibilityTime);
        canBeHit = true;
    }


    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        body.interpolation = RigidbodyInterpolation2D.Interpolate;
        originalScale = transform.localScale;
        // helps jitter
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
            {
            jumpPressed = true;
            }
    }

    private void FixedUpdate()
    {
        if (isControlledCharacter)
        {
            // Horizontal movement only
            body.linearVelocity = new Vector2(movementInput.x * speed, body.linearVelocity.y);

            animator.SetFloat("Speed", Mathf.Abs(movementInput.x));
            animator.SetBool("Grounded", canJump);

            // Jump (apply once)
            if (jumpPressed && canJump)
            {
                body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce);
                canJump = false;
            }
            jumpPressed = false;

            // Flip sprite
            if (movementInput.x > 0.01f)
                transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
            else if (movementInput.x < -0.01f)
                transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
{
    // --- Ground check ---
    if (collision.gameObject.CompareTag("Platfroms"))
    {
        canJump = true;
        return; // stop here; don't treat platform as enemy hit
    }

    // --- Hit logic ---
    Debug.Log(
        $"[COLLISION] self={gameObject.name} tag={gameObject.tag} " +
        $"other={collision.gameObject.name} otherTag={collision.gameObject.tag} " +
        $"myCol={collision.otherCollider.name} otherCol={collision.collider.name}"
    );

    if (!CompareTag("Player")) return;
    if (!collision.gameObject.CompareTag("Enemy")) return;

    if (Time.time < nextAllowedHitTime) return;
    nextAllowedHitTime = Time.time + hitCooldown;

    string trig = nextHitIsB ? "Hit2" : "Hit";
    Debug.Log($"[REGISTER HIT] {gameObject.name} triggers {trig} on self and {collision.gameObject.name}");

    animator.SetTrigger(trig);

    Animator otherAnim = collision.gameObject.GetComponent<Animator>();
    if (otherAnim != null) otherAnim.SetTrigger(trig);

    PlayHitEffect();



    nextHitIsB = !nextHitIsB;
}

private void OnCollisionExit2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("Platfroms"))
    {
        canJump = false;
    }
}



}
