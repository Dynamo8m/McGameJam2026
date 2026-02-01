using UnityEngine;

public class PlayerForm : MonoBehaviour
{
    [SerializeField] private bool isDemon = false;
    public bool IsDemon => isDemon;
    public bool IsHuman => !isDemon;

    // Call this when you "swap" forms
    public void SetDemon(bool demon)
    {
        isDemon = demon;
    }

    public void Toggle()
    {
        isDemon = !isDemon;
    }
}
