using UnityEngine;
using UnityEngine.Events;
//獣娃 蒸陥たたたたたたたた戚開ぞ�Ч造咾ご欸�馬幾じ巨指びぉ

public class Unit : MonoBehaviour
{
    [SerializeField] private GameObject[] active;
    public UnityEvent Event;

    public void SetSelected(bool isSelected)
    {
        for(int i=0;i<active.Length; i++)
        {
            active[i].SetActive(isSelected);
        }
    }

    public void IsArrived()
    {
        Event?.Invoke();
    }
}
