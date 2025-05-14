using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class EffectTaskWaiting : MonoBehaviour
{
    private static bool Flag = false;
    [SerializeField] GameObject obj;
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    void Start()
    {
        if (!Flag)
        {
            obj.GetComponent<VideoPlayer>().Play();
        }
    }

}
