using UnityEngine;
using UnityEngine.Events;
//½Ã°£ ¾ø´Ù¤¿¤¿¤¿¤¿¤¿¤¿¤¿¤¿ÀÌ³«¤¾‘§¹Â¤Ó¤¤´Ý·ùÇÏ´ö¤¸µð»Ø¤Ó¤©

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
