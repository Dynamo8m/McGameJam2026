using UnityEngine;
using UnityEngine.UI;

public class ChangeImage : MonoBehaviour
{
    public Sprite buttonHoverImage;
    public Button button;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ChangeButtonImage() 
    {
        button.image.sprite = buttonHoverImage;
    }
}
