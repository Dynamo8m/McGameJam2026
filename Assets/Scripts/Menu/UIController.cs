using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    public Transform MainCanvas;

    void Start()
    {
        if (Instance != null) {
            GameObject.Destroy(this.gameObject);
            return;
        }

        Instance = this;
    }

    public OptionController CreateOptionController() 
    {
        GameObject Popup = Instantiate(Resources.Load("UI/Pop-up") as GameObject);

        return Popup.GetComponent<OptionController>();
    }
}
