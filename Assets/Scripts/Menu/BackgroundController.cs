using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundController : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private string gameSceneName;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void playRip()
    {
        animator.SetTrigger("Start");
    }

    public void TransitionScene()
    {
        GameObject oldParent = this.transform.parent.gameObject;
        this.transform.parent = this.transform.parent.parent;
        oldParent.transform.DetachChildren();
        DontDestroyOnLoad(this.gameObject);
        SceneManager.LoadScene(gameSceneName);
        
    }

    public void Remove()
    {
        Destroy(this.gameObject);
    }
}
