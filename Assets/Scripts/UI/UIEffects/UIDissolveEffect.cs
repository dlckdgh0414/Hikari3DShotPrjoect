using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class UIDissolveEffect : MonoBehaviour
{
    VideoPlayer vid;
    [SerializeField] List<GameObject> obj;
    public float time = 12f;
    private void Awake()
    {
        vid = GetComponent<VideoPlayer>();
        vid.Prepare();

        StartCoroutine(ShowUIEffect());
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
