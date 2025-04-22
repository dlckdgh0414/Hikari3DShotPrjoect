using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class UIDissolveEffect : MonoBehaviour
{
    VideoPlayer vid;
    [SerializeField] List<GameObject> obj;
    private void Awake()
    {
        vid = GetComponent<VideoPlayer>();
        vid.Prepare();

        StartCoroutine(ShowUIEffect());
    }

    IEnumerator ShowUIEffect()
    {
        yield return new WaitForSeconds(12f);

        foreach (GameObject item in obj)
        {
            item.SetActive(false);
        }

        vid.Play();
    }
}
