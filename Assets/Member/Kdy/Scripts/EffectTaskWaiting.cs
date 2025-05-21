using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class EffectTaskWaiting : MonoBehaviour
{
    private static bool Flag = false;
    [SerializeField] MinimalWait waitcanvas;
    [SerializeField] List<GameObject> obj;
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    void Start()
    {
        if (!Flag)
        {
            Flag = true;
        }

        else
        {
            waitcanvas.wait = 0.1f;
            foreach (GameObject item in obj)
            {
                item.SetActive(false);
            }
        }
    }

}
