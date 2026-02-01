using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitGate : MonoBehaviour
{
    [SerializeField] private string nextSceneName = "Hell_2";

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Only let Player objects try
        if (!other.CompareTag("Player")) return;

        // IMPORTANT: collider might be on a child, so grab Animator from parent too
        Animator anim = other.GetComponentInParent<Animator>();
        if (anim == null)
        {
            Debug.LogWarning("[EXIT] Player entered but no Animator found in parent.");
            return;
        }

        bool isDemon = anim.GetBool("IsDemon");
        Debug.Log($"[EXIT] {other.name} entered. IsDemon={isDemon}");

        // Only humans can exit
        if (isDemon) return;

        SceneManager.LoadScene(nextSceneName);
    }
}
