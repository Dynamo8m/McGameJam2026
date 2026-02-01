using UnityEngine;
using UnityEngine.UI;
using System;

public class OptionController : MonoBehaviour
{
    [SerializeField] Button button;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Init(Transform canvas) 
    {
        transform.SetParent(canvas);
        transform.localScale = Vector3.one;
        GetComponent<RectTransform>().offsetMin = Vector2.zero;
        GetComponent<RectTransform>().offsetMax = Vector2.zero;

        button.onClick.AddListener(() => {
            GameObject.Destroy(this.gameObject);
        });
    }
}
