using System;
using UnityEngine;
using UnityEngine.UI;

public class EqumentBtn : MonoBehaviour
{
    private Button thisBtn;
    private Image thisImg;
    private void Awake()
    {
        thisImg = GetComponent<Image>();    
        thisBtn = GetComponent<Button>();
        thisBtn.onClick.AddListener(ClickThis);
    }

    private void ClickThis()
    {
        if (thisImg.sprite != null)
            thisImg.sprite = null;
    }
    
    
}
