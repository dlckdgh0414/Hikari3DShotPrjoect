using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Select : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private List<GameObject> obj;
    private bool isActive = false;

    void Start()
    {
        foreach (GameObject item in obj)
        {
            item.SetActive(false);
        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
            foreach (GameObject item in obj)
            {
                item.SetActive(true);
            }
            isActive = true;
        
    }
}