using UnityEngine;
using UnityEngine.UI;
using System;

public class OptionPopupActivate : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() => {
            OptionController popup = UIController.Instance.CreateOptionController();
            popup.Init(UIController.Instance.MainCanvas);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
