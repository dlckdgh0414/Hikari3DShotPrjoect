using UnityEngine;
using UnityEngine.Events;

public class Unit : MonoBehaviour
{
    [SerializeField] private GameObject activeCircle;
    public UnityEvent Event;

    public void SetSelected(bool isSelected)
    {
        activeCircle.SetActive(isSelected);
    }

    public void IsArrived()
    {
        Event?.Invoke();
    }
}
