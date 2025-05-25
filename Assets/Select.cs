using UnityEngine;
using UnityEngine.EventSystems;

public class Select : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameObject obj;

    private bool isActive = false;

    void Start()
    {
        obj.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isActive)
        {
            obj.SetActive(true);
            isActive = true;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isActive && !EventSystem.current.IsPointerOverGameObject())
        {
            obj.SetActive(false);
            isActive = false;
        }
    }
}