using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitGate : MonoBehaviour
{
    [SerializeField] private string nextSceneName = "Hell_2";

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Get the character body that owns this collider (prevents wrong-animator bugs)
        Rigidbody2D rb = other.attachedRigidbody;
        if (rb == null) return;

        Animator anim = rb.GetComponentInChildren<Animator>();
        if (anim == null)
        {
            Debug.LogWarning($"[EXIT] owner={rb.name} but no Animator found under it.");
            return;
        }

        bool isHumanAnim = false;
        bool isDemonAnim = false;

        // Check all layers + both current/next states (handles transitions)
        for (int layer = 0; layer < anim.layerCount; layer++)
        {
            var cur = anim.GetCurrentAnimatorStateInfo(layer);
            if (cur.IsTag("Human")) isHumanAnim = true;
            if (cur.IsTag("Demon")) isDemonAnim = true;

            if (anim.IsInTransition(layer))
            {
                var nxt = anim.GetNextAnimatorStateInfo(layer);
                if (nxt.IsTag("Human")) isHumanAnim = true;
                if (nxt.IsTag("Demon")) isDemonAnim = true;
            }
        }

        Debug.Log($"[EXIT] owner={rb.name} humanTag={isHumanAnim} demonTag={isDemonAnim}");

        
        // Fail-safe: if demonTag is true OR humanTag is false -> do not exit
        if (isDemonAnim) return;
        if (!isHumanAnim) return;

        SceneManager.LoadScene(nextSceneName);
    }
}
