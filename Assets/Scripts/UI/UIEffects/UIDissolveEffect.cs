using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class UIDissolveEffect : MonoBehaviour
{
    VideoPlayer vid;
    [SerializeField] List<GameObject> obj;
    private static bool Flag = false;
    public float time = 12f;
    private void Awake()
    {
        vid = GetComponent<VideoPlayer>();
        vid.Prepare();
        vid.prepareCompleted += Cutscene;

    }


    private void Cutscene(VideoPlayer source)
    {
        if (!Flag && source != null)
        {
            Flag = true;
            StartCoroutine(ShowUIEffect());
        }
        else
        {
            foreach (GameObject item in obj)
            {
                item.SetActive(false);
            }
        }
    }

    IEnumerator ShowUIEffect()
    {
        yield return new WaitForSeconds(time);

        foreach (GameObject item in obj)
        {
            item.SetActive(false);
        }

        vid.Play();
    }
}
